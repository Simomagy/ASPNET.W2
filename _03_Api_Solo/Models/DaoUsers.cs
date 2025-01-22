using MSSTU.DB.Utility;

namespace _01_Auth.Models;

public class DaoUsers
{
    #region Singleton

    private readonly Database _db;
    private static DaoUsers? _instance;

    private DaoUsers()
    {
        _db = new Database("LoginDOITA14");
    }

    public static DaoUsers GetInstance()
    {
        return _instance ??= new DaoUsers();
    }

    #endregion

    public bool CreateRecord(User entity)
    {

        if (entity.Propic == "")
            entity.Propic = "https://i.imgur.com/K1AwRC5.png";

        var loginsParameters = new Dictionary<string,object>
        {
            {"@user",entity.Username.Replace("'", "''")},
            {"@role",entity.Role.Replace("'", "''")},
        };

        var accountDataParameters = new Dictionary<string,object>
        {
            {"@name",entity.Name.Replace("'", "''")},
            {"@surname",entity.Surname.Replace("'", "''")},
            {"@dob",entity.Dob},
            {"@propic", entity.Propic.Replace("'", "''")},
        };

        var pwd = entity.Pwd;
        var loginQuery = "Insert INTO Logins (username, pwd, role) " +
                             $"VALUES (@user, HASHBYTES('SHA2_512', '{pwd}'), @role)";

        const string accountDataQuery = "INSERT INTO AccountsData (name, surname, dob, propic) VALUES " +
                                        "(@name, @surname, @dob, @propic)";

        var loginResponse = _db.UpdateDb(loginQuery, loginsParameters);
        var accountDataResponse = _db.UpdateDb(accountDataQuery, accountDataParameters);

        return loginResponse && accountDataResponse;
    }

    public bool DeleteRecord(string username, string pwd)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@username", username }
        };
        string query = $"DELETE FROM Logins WHERE username = @username AND pwd = HASHBYTES('SHA2_512', '{pwd}')";

        return _db.UpdateDb(query,parameters);
    }

    public List<User> GetRecords()
    {
        const string query = "SELECT " +
                             "l.username, l.pwd, l.role, " +
                             "a.name, a.surname, a.dob, a.propic " +
                             "FROM Logins l JOIN AccountsData a ON l.id = a.id";
        List<User> entities = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return entities;

        foreach( var singleResponse in fullResponse)
        {
            var user = new User();
            user.TypeSort(singleResponse);

            entities.Add(user);
        }
        return entities;
    }

    public bool UserExists(string user, string pwd)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@user", user }
        };
        string query = $"select * from Logins where username = @user and pwd = hashbytes('SHA2_512', '{pwd}')";
        return _db.ReadOneDb(query, parameters) != null;
    }

    public User FindUser(string userClient)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@user", userClient }
        };
        const string query = "SELECT " +
                             "l.username, l.role, " +
                             "a.name, a.surname, a.dob, a.propic " +
                             "FROM Logins l JOIN AccountsData a ON l.id = a.id WHERE l.username = @user";

        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;

        var user = new User();
        user.TypeSort(singleResponse);

        return user;
    }
}
