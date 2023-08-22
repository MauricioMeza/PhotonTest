using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using System;
using UnityEngine.SceneManagement;

public class Auth : MonoBehaviour
{
    [Header("--Login Fields")]
    [SerializeField] GameObject loginScreen;
    [SerializeField] TMP_InputField userMail;
    [SerializeField] TMP_InputField userPass;
    [SerializeField] TMP_Text userDebug;

    [Header("--Register Fields")]
    [SerializeField] GameObject regScreen;
    [SerializeField] TMP_InputField regMail;
    [SerializeField] TMP_InputField regPass;
    [SerializeField] TMP_InputField regPass2;
    [SerializeField] TMP_Text regDebug;

    [Header("--Welcome")]
    [SerializeField] GameObject welcScreen;
    [SerializeField] TMP_Text welcDebug;


    //------------------------------
    //-----------REGISTRO-----------
    //------------------------------
    public void RegisterUser(){
        if(regMail.text.Length == 0 || regPass.text.Length == 0 || regPass2.text.Length == 0){
            regDebug.text = "Llena todos los campos";
            return;    
        }
        if(regPass.text.Length < 6 ){
            regDebug.text = "La contraseña debe tener por lo menos 6 caracteres";
            return;    
        }
        if(regPass.text != regPass2.text){
            regDebug.text = "Las contraseñas no coinciden";
            return;
        }
        
        StartCoroutine(Register());
    }
    IEnumerator Register(){
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        var res = auth.CreateUserWithEmailAndPasswordAsync(regMail.text, regPass.text); 
        yield return new WaitUntil(() => res.IsCompleted);

        if(res.IsCompletedSuccessfully){
            regDebug.text = "Registro exitoso, ahora ya puedes ingresar";
            WelcomeUser();
        }else{
            regDebug.text = res.Exception.Message;
        }
    }


    //----------------------------
    //-----------LOGIN------------
    //----------------------------
    public void LoginUser(){
        StartCoroutine(Login());
    }
    IEnumerator Login(){
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        var res = auth.SignInWithEmailAndPasswordAsync(userMail.text, userPass.text);   
        yield return new WaitUntil(() => res.IsCompleted);

        if(res.IsCompletedSuccessfully){
            userDebug.text = "Usuario Ingresado";
            WelcomeUser();
        }else{
            userDebug.text = res.Exception.Message;
        }
    }
    public void WelcomeUser(){
        welcDebug.text = "Hola " + FirebaseAuth.DefaultInstance.CurrentUser.Email + ", en unos momentos te redirigiremos a una partida.";
        loginScreen.SetActive(false);
        regScreen.SetActive(false);
        welcScreen.SetActive(true);
        StartCoroutine(ChangeScene());
        
    }
    IEnumerator ChangeScene(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);    
    }
}
