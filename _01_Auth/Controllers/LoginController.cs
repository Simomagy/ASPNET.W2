using _01_Auth.Models;
using Microsoft.AspNetCore.Mvc;
using MSSTU.DB.Utility;

namespace _01_Auth.Controllers;

public class LoginController : Controller
{
    // Mi serve tenere in memoria chi ha fatto il Login e quanti tentativi di accesso ha fatto
    private static int _loginAttempts;
    static User? _loggedUser;

    // Usiamo ILogger
    // ILogger vuole come argomento il tipo della classe che lo usa
    ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation($"Tentativo di accesso n. {_loginAttempts} - {DateTime.Now}");
        return View(_loginAttempts);
    }

    public IActionResult Login(Dictionary<string, string> parameters)
    {
        string username = parameters["email"];
        string password = parameters["password"];

        var userExists = DaoUsers.GetInstance().UserExists(username, password);
        if (userExists)
        {
            Entity user = DaoUsers.GetInstance().FindUser(username);
            _loggedUser = (User)user;
            return View(user);
        }
        _loginAttempts++;
        return RedirectToAction("Index");
    }

    public IActionResult Logout()
    {
        _logger.LogInformation($"Logout di {_loggedUser?.Username} - {DateTime.Now}");
        _loggedUser = null;
        _loginAttempts = 0;
        return RedirectToAction("Index");
    }

    public IActionResult Signup()
    {
        return View();
    }

    public IActionResult Register(Dictionary<string, string> parameters)
    {
        Entity user = new User();
        user.TypeSort(parameters);
        ((User)user).Role = "user";
        var result = DaoUsers.GetInstance().CreateRecord(user);
        _loginAttempts = 0;
        return RedirectToAction(result ? "Index" : "Signup");
    }
}
