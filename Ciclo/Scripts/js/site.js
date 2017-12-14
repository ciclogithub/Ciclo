$(document).ready(function () {

    $("#btn_solicitar_inscricao").click(function () {
        SolicitarInscricao();
    });

    $("#btn_alterar_senha").click(function () {
        $('#form-modal_senha').validationEngine('attach');
        if ($('#form-modal_senha').validationEngine('validate')) {
            AlterarSenha();
        }
    });

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
            LoginSite();
        }
    });
});

function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};

function recuperarSenha() { 
    swal({
        title: 'Informe o e-mail cadastrado',
        input: 'email',
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonText: 'Solicitar',
        showLoaderOnConfirm: true,
        inputPlaceholder: 'E-mail',
        inputValidator: (value) => {
            if (!value) {
                return 'Preencha o e-mail corretamente';
            }
        },
        allowOutsideClick: false
    }).then((result) => {
        if (result.value) {
            valor = result.value;
            if (isValidEmailAddress(valor)) {
                $.ajax({
                    type: "POST",
                    url: "/Home/Esqueceu/",
                    data: { email: valor, perfil: $("#perfil:checked").val() },
                    dataType: "json",
                    traditional: true,
                    async: false,
                    success: function (retorno) {
                        if (retorno.retorno == 0) {
                            swal({ title: retorno.mensagem, type: "error", timer: 3000 });
                        } else {
                            swal({ title: 'Mensagem enviada para o e-mail cadastrado.', type: "success", timer: 3000 });
                        }
                    }
                });
            } else {
                swal({ title: 'E-mail inválido', type: "error", timer: 3000 });
            }
        }
    })

}

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
        $.ajax({
            type: "POST",
            url: "/Cadastro/Formulario/",
            data: $('#form_cadastro_organizador :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {     
                if (retorno.id == 0) {
                    swal({ title: retorno.mensagem, type: "error", timer: 3000 });
                } else {
                    window.location = "/Painel";
                    swal({
                        title: retorno.mensagem,
                        type: 'success',
                        confirmButtonText: 'Fechar',
                    })  
                }
            }
        });
    } else {
        swal({ title: (erroMsg2 == '' ? 'Preencha ' + erroMsg + ' corretamente. ' : erroMsg2), type: "error", timer: 3000 });
    }
}

function CadastroAluno() {

    var usuario = $("#form_cadastro_aluno #txusuario").val();
    var email = $("#form_cadastro_aluno #txemail").val();
    var senha = $("#form_cadastro_aluno #txsenhaaluno").val();
    var confsenha = $("#form_cadastro_aluno #txconfsenhaaluno").val();

    var erro = false;
    var erroMsg = "";
    var erroMsg2 = "";

    if (usuario.length < 2) {
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
        $.ajax({
            type: "POST",
            url: "/Cadastro/FormularioAluno/",
            data: $('#form_cadastro_aluno :input').serialize(),
            dataType: "json",
            traditional: true,
            success: function (retorno) {
                if (retorno.id == 0) {
                    swal({ title: retorno.mensagem, type: "error", timer: 3000 });
                } else {
                    window.location = "/Aluno";
                    swal({
                        title: retorno.mensagem,
                        type: 'success',
                        confirmButtonText: 'Fechar',
                    })                    
                }
            }
        });
    } else {
        swal({ title: (erroMsg2 == '' ? 'Preencha ' + erroMsg + ' corretamente. ' : erroMsg2), type: "error", timer: 3000 });
    }
}

function LoginSite() {

    var email = $("#txemail").val();
    var curso = $("#curso").val();
    var perfil = $("#perfil:checked").val();
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

    if (perfil == 1) {
        url = "/Login/Formulario/";
        link = "/Painel";
    } else {
        url = "/Login/FormularioAluno/";
        if ((curso == 0) || (curso == "")) { link = "/Aluno"; } else { link = "/Inscricao" }
    }

    if (!erro) {
        $("#form-error").addClass("bg-info").html('Aguarde...');
        $.ajax({
            type: "POST",
            url: url,
            data: $('#form_login_organizador :input').serialize() + '&' + $.param({ 'txsenhaaluno': senha }),
            dataType: "json",
            traditional: true,
            success: function (msg) {
                var json = $.parseJSON(msg);
                
                if (json.retorno == "OK") {
                    window.location = link;
                    swal({
                        title: "Redirecionando...",
                        type: 'success',
                        confirmButtonText: 'Fechar',
                    })
                }

                if (json.retorno == "Dados incorretos") {
                    swal({ title: json.retorno, type: "error", timer: 3000 });

                }

                if (json.retorno == "Usuário desativado, por favor entre em contato") {
                    swal({ title: json.retorno, type: "error", timer: 3000 });
                }
            }
        });
    } else {
        swal({ title: "Preencha " + erroMsg + " corretamente.", type: "error", timer: 3000 });
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

function AlterarSenha() {

    var perfil = $("#form-modal_senha #perfil").val();
    var usuario = $("#form-modal_senha #usuario").val();
    var codigo = $("#form-modal_senha #codigo").val();
    var txnovasenha = $("#form-modal_senha #txnovasenha").val();
    var txconfirmasenha = $("#form-modal_senha #txconfirmasenha").val();

    $.ajax({
        type: "POST",
        url: "/AlteraSenha/NovaSenha/",
        data: { perfil: perfil, usuario: usuario, codigo: codigo, txnovasenha: txnovasenha, txconfirmasenha: txconfirmasenha },
        dataType: "json",
        traditional: true,
        success: function (retorno) {
            if (retorno.id == 0) {
                swal({ title: retorno.mensagem, type: "error", timer: 3000 });
            } else {
                swal({
                    title: retorno.mensagem,
                    type: 'success',
                    confirmButtonText: 'Fechar',
                }).then((result) => {
                    alert(result.value)
                    if (result.value) {
                        window.location = "/Login";
                    }
                })
            }
        }
    });
}

function SolicitarInscricao() {

    var curso = $("#form_inscricao #curso").val();
    if (curso == "") {
        swal({ title: "Erro ao solicitar a inscrição, recarregue a página", type: "error", timer: 3000 });
    } else {
        $("#form_inscricao").submit();        
    }
}