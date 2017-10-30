$(document).ready(function () {

    $("#btn_cadastro_organizador").click(function () {
        $('#form_cadastro_organizador').validationEngine('attach');
        if ($('#form_cadastro_organizador').validationEngine('validate')) {
            CadastroOrganizador();
        }
    });

    $("#btn_login_organizador").click(function () {
        $('#form_login_organizador').validationEngine('attach');
        if ($('#form_login_organizador').validationEngine('validate')) {
            LoginOrganizador();
        }
    });

});

function CadastroOrganizador() {

    var organizador = $("#txorganizador").val();
    var email = $("#txemail").val();
    var telefone = $("#txtelefone").val();
    var senha = $("#txsenha").val();
    var confsenha = $("#txconfsenha").val();
    var descricao = $("#txdescritivo").val();

    var erro = false;
    var erroMsg = "";
    var erroMsg2 = "";

    if (organizador.length < 2) {
        erro = true;
        erroMsg += "organizador";
    }

    if (email.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "e-mail";
    }

    if (telefone.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "telefone";
    }

    if (senha.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "senha";
    }

    if (senha != confsenha) {
        erro = true;
        erroMsg2 += "Senha e confirmação devem ser iguais";
    }

    if (!erro) {
        $("#form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: "/Cadastro/Formulario/",
            data: $('#form_cadastro_organizador :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {              
                if (retorno.id == 0) {
                    $("#form-error").addClass("bg-danger").html(retorno.mensagem);
                } else {
                    $("#form-error").addClass("bg-success").html(retorno.mensagem);
                    window.location = "/Painel";
                }
            }
        });
    } else {
        $("#form-error").addClass("bg-danger").html((erroMsg2 == '' ? 'Preencha ' + erroMsg + ' corretamente. ' : erroMsg2));
    }
}

function LoginOrganizador() {

    var email = $("#txemail").val();
    var senha = $("#txsenha").val();

    var erro = false;
    var erroMsg = "";

    if (email.length < 2) {
        erro = true;
        erroMsg += "e-mail";
    }

    if (senha.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "senha";
    }

    if (!erro) {
        $("#form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: "/Login/Formulario/",
            data: $('#form_login_organizador :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (msg) {
                var json = $.parseJSON(msg);

                if (json.retorno === "OK") {
                    $("#form-error").addClass("bg-success").html(json.retorno);
                    window.location = "/Painel";
                }

                if (json.retorno === "Dados incorretos") {
                    $("#form-error").addClass("bg-danger").html(json.retorno);
                }

                if (json.retorno === "Usuário desativado, por favor entre em contato") {
                    $("#form-error").addClass("bg-danger").html(json.retorno);
                }
            }
        });
    } else {
        $("#form-error").addClass("bg-danger").html('Preencha ' + erroMsg + ' corretamente.');
    }
}