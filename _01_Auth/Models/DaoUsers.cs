using MSSTU.DB.Utility;

namespace _01_Auth.Models;

public class DaoUsers : IDAO
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

    public bool CreateRecord(Entity entity)
    {

        var parameters = new Dictionary<string,object>
        {
            {"@user",((User)entity).Username.Replace("'", "''")},
            {"@role",((User)entity).Role.Replace("'", "''")}
        };
        var pwd = ((User)entity).Pwd;
        var query = "Insert INTO Logins (username, pwd, role) " +
                             $"VALUES (@user, HASHBYTES('SHA2_512', '{pwd}'), @role)";

        return _db.UpdateDb(query, parameters);
    }

    public bool DeleteRecord(int recordId)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@id", recordId }
        };
        const string query = "DELETE FROM Logins WHERE id = @id";

        return _db.UpdateDb(query,parameters);
    }
    public Entity? FindRecord(int recordId)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@id", recordId }
        };

        const string query = "SELECT * FROM Logins WHERE id = @id";

        var singleResponse = _db.ReadOneDb(query, parameters);
        if(singleResponse == null)
            return null;

        Entity user = new User();
        user.TypeSort(singleResponse);

        return user;

    }

    public List<Entity> GetRecords()
    {
        const string query = "SELECT * FROM Logins";
        List<Entity> entities = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return entities;

        foreach( var singleResponse in fullResponse)
        {
            Entity user = new User();
            user.TypeSort(singleResponse);

            entities.Add(user);
        }
        return entities;
    }

    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string,object>
        {
            {"@id",((User)entity).Id},
            {"@user",((User)entity).Username.Replace("'", "''")},
            {"@role",((User)entity).Role.Replace("'", "''")}
        };
        var pwd = ((User)entity).Pwd;
        var query = $"UPDATE Logins SET [user] = @user, pwd = HASHBYTES('SHA2_512', '{pwd}'), role = @role WHERE id = @id ";

        return _db.UpdateDb(query, parameters);
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

    public Entity FindUser(string userClient)
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

        Entity user = new User();
        user.TypeSort(singleResponse);

        return user;
    }
}
