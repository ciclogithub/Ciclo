$(document).ready(function () {

    $("#btn_cadastro_empresa").click(function () {
        CadastroEmpresa();
    });

    $("#btn_login").click(function () {
        LoginEmpresa();
    });
});

function CadastroEmpresa() {
    
    var empresa = $("#txempresa").val();
    var email = $("#txemail").val();
    var telefone = $("#txtelefone").val();
    var senha = $("#txsenha").val();
    var descricao = $("#txdescritivo").val();

    var erro = false;
    var erroMsg = "";

    if (empresa.length < 2) {
        erro = true;
        erroMsg += "Nome da Empresa/Instrutor";
    }

    if (email.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "E-mail";
    }

    if (telefone.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "Telefone";
    }

    if (senha.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "Senha";
    }

    
    if (!erro) {
        $("#form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: "/Cadastro/Formulario/",
            data: $('#form_cadastro_empresa :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {
                if (retorno.retorno == 0) {
                    $("#form-error").addClass("bg-danger").html(retorno.mensagem);
                } else {
                    $("#form-error").addClass("bg-success").html(retorno.mensagem);
                    window.location = "/Painel";
                }
            }
        });
    } else {
            $("#form-error").addClass("bg-danger").html('Preencha ' + erroMsg + ' corretamente.');
    }
}

function LoginEmpresa() {

    var email = $("#txemail").val();
    var senha = $("#txsenha").val();

    var erro = false;
    var erroMsg = "";

    if (email.length < 2) {
        erro = true;
        erroMsg += "E-mail";
    }

    if (senha.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "Senha";
    }

    if (!erro) {
        $("#form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: "/Login/Formulario/",
            data: $('#form_login :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {
                if (retorno.retorno == 0) {
                    $("#form-error").addClass("bg-danger").html(retorno.mensagem);
                } else {
                    $("#form-error").addClass("bg-success").html(retorno.mensagem);
                    //window.location = "/Cadastro/Formulario/" + retorno.idempresa;
                }
            }
        });
    } else {
        $("#form-error").addClass("bg-danger").html('Preencha ' + erroMsg + ' corretamente.');
    }
}