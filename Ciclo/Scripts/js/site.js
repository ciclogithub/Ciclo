$(document).ready(function () {

    $("#esqueceu").click(function () {
        $("#form_envio").toggle();
    });

    $("#CadastroEnviaSenha").click(function () {
        CadastroEnviaSenha();
    })

    $("#btn_cadastro_aluno").click(function () {
        $('#form_cadastro_aluno').validationEngine('attach');
        if ($('#form_cadastro_aluno').validationEngine('validate')) {
            CadastroAluno();
        }
    });

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

    var organizador = $("#form_cadastro_organizador #txorganizador").val();
    var email = $("#form_cadastro_organizador #txemail").val();
    var telefone = $("#form_cadastro_organizador #txtelefone").val();
    var senha = $("#form_cadastro_organizador #txsenha").val();
    var confsenha = $("#form_cadastro_organizador #txconfsenha").val();
    var descricao = $("#form_cadastro_organizador #txdescritivo").val();

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
       
        $("#form_cadastro_organizador #form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: "/Cadastro/Formulario/",
            data: $('#form_cadastro_organizador :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {     
                if (retorno.id == 0) {
                    $("#form_cadastro_organizador #form-error").addClass("bg-danger").html(retorno.mensagem);
                } else {
                    $("#form_cadastro_organizador #form-error").addClass("bg-success").html(retorno.mensagem);
                    window.location = "/Painel";
                }
            }
        });
    } else {
        $("#form_cadastro_organizador #form-error").addClass("bg-danger").html((erroMsg2 == '' ? 'Preencha ' + erroMsg + ' corretamente. ' : erroMsg2));
    }
}

function CadastroAluno() {

    var aluno = $("#form_cadastro_aluno #txaluno").val();
    var email = $("#form_cadastro_aluno #txemail").val();
    var senha = $("#form_cadastro_aluno #txsenhaa").val();
    var confsenha = $("#form_cadastro_aluno #txconfsenhaa").val();

    var erro = false;
    var erroMsg = "";
    var erroMsg2 = "";

    if (aluno.length < 2) {
        erro = true;
        erroMsg += "aluno";
    }

    if (email.length < 2) {
        erro = true;
        if (erroMsg != "")
            erroMsg += ", ";
        erroMsg += "e-mail";
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
        $("#form_cadastro_aluno #form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: "/Cadastro/FormularioAluno/",
            data: $('#form_cadastro_aluno :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {
                if (retorno.id == 0) {
                    $("#form_cadastro_aluno #form-error").addClass("bg-danger").html(retorno.mensagem);
                } else {
                    $("#form_cadastro_aluno #form-error").addClass("bg-success").html(retorno.mensagem);
                    window.location = "/Aluno";
                }
            }
        });
    } else {
        $("#form_cadastro_aluno #form-error").addClass("bg-danger").html((erroMsg2 == '' ? 'Preencha ' + erroMsg + ' corretamente. ' : erroMsg2));
    }
}

function LoginOrganizador() {

    var email = $("#txemail").val();
    var senha = $("#txsenha").val();
    var perfil = $("#perfil:checked").val();

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

    if (perfil == 1) {
        url = "/Login/Formulario/";
        link = "/Painel";
    } else {
        url = "/Login/FormularioAluno/";
        link = "/Aluno";
    }


    if (!erro) {
        $("#form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: url,
            data: $('#form_login_organizador :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (msg) {
                var json = $.parseJSON(msg);

                if (json.retorno === "OK") {
                    $("#form-error").addClass("bg-success").html(json.retorno);
                    window.location = link;
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

function CadastroEnviaSenha() {
    var email = $("#email_envio").val();

    var erro = false;
    var erroMsg = "";

    if (email.length < 5) {
        erro = true;
        erroMsg += "E-mail";
    }

    $("#form-error").removeClass().html('');

    if (!erro) {
        $.ajax({
            type: "POST",
            url: "/Home/Esqueceu/",
            data: { esqueceu: email },
            dataType: "json",
            traditional: true,
            success: function (retorno) {
                if (retorno.retorno == 0) {
                    $("#form-error").addClass("alert alert-danger").html(retorno.mensagem);
                } else {
                    $("#form-error").addClass("alert alert-success").html('Enviado para o e-mail cadastrado.');
                }
            }
        });
    } else {
        $("#form-error").addClass("alert alert-danger").html('Preencha ' + erroMsg + ' corretamente.');
    }

}

function mudaPerfil(id) {
    if (id == 1) {
        $("#form_cadastro_organizador").removeClass("hide");
        $("#form_cadastro_aluno").addClass("hide");
    } else {
        $("#form_cadastro_organizador").addClass("hide");
        $("#form_cadastro_aluno").removeClass("hide");
    }
}