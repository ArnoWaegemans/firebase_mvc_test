using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//ik heb firebase.auth en Microsoft.AspNetCore.Session geinstalleerd op alle lagen maar is wss alleen nodig op deze laag
//ook in de startup paar dingen toegevoegd voor de sessie variabele

namespace FirestoreTest.Controllers
{
    public class LoginController : Controller
    {
        //provider met de api key
        FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDCJLY-m61GNm7RJye8QHlpHX3eTe05eRg"));
        public LoginController() {
            
        }

        public async Task<IActionResult> Index()
        {            
            try
            {
                //registreren met email/passwd
                //authProvider.CreateUserWithEmailAndPasswordAsync("test@test.com", "abc123");

                //inloggen met username/passwd
                var fbAuthLink = await authProvider.SignInWithEmailAndPasswordAsync("test@test.com", "abc123");
                string token = fbAuthLink.FirebaseToken;

                //token opslaan in een sessie variabele om te kunnen controleren of een user ingelogd is 
                HttpContext.Session.SetString("_UserToken", token);         
                
            }
            catch (Exception ex){}



            //zou in een andere view of controller kunnen staan.

            //user token uit de sessievariabele ophalen
            var userToken = HttpContext.Session.GetString("_UserToken");         

            //controleren of er een usertoken is 
            if (userToken != null)
            {
                //controleren of er een user teruggevonden wordt / user informatie ophalen
                //wss alleen nodig als je de user data nodig hebt zoals hier
                User user = await authProvider.GetUserAsync(userToken);
                if (user != null)
                {
                    return View(user);
                }
                else {
                    return Unauthorized();
                }
                
            }
            else {
                return Unauthorized();
            }

            //om met roles te werken zou je in firestore een tabel kunnen maken met de web users waarin hun gebruikersid staat samen met de rol bv admin/clubverantw/politie
            //dan kun je dat toevoegen als controle bv voor het weergeven van een pagina
           
        }
    }
}
