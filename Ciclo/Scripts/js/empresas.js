$(document).ready(function () {

    $("#pesquisa_empresa").click(function () {
        AlunoPesquisar();
    });

    $("#incluir_btn_empresa").click(function () {
        var err = false;
        $('#form-modal_empresa').validationEngine('attach');
        if ($('#form-modal_empresa').validationEngine('validate')) {
            if (!err) { IncluirEmpresa(); }
        };
    });
});

function pagination(c) {
    var p = $("#page").val();
    var t = $("#totalpage").val();
    if (c == -1) {
        c = parseInt(p) - 1;
        if (c <= 0) { c = 1 }
        window.location = "/Painel/Empresas/?pagina=" + c + "&empresa=" + $("#empresa").val();
    } else {
        if (c == 0) {
            c = parseInt(p) + 1;
            if (c > t) { c = t }
            window.location = "/Painel/Empresas/?pagina=" + c + "&empresa=" + $("#empresa").val();
        } else {
            window.location = "/Painel/Empresas/?pagina=" + c + "&empresa=" + $("#empresa").val();
        }
    }
}

function EmpresasTodos() {
    window.location = "/Painel/Empresas/?pagina=1&empresa=";
}

function EmpresaPesquisar() {
    window.location = "/Painel/Empresas/?empresa=" + $("#empresa").val();
}

function Empresas(id) {
    Modal("/Painel/Empresas/Incluir", id, "Empresas", "");
}

function EmpresaAlterar() {

    cont = 0;
    val = 0;
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            cont++;
            val = $(this).val();
        }
    });

    if (cont === 0) {
        alert("Selecione pelo menos 1 registro")
    } else {
        if (cont > 1) {
            alert("Selecione somente 1 registro para alterar")
        } else {
            Alunos(val);
        }
    }

}

function EmpresaExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val()
        }
    });

    var ids = ids.substring(1);

    if (ids !== "") {
        if (confirm("Certeza que deseja excluir o(s) registro(s) selecionado(s)?")) {
            $.ajax({
                type: "POST",
                url: "/Painel/Empresas/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#empresa").val("");
                    EmpresaPesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirEmpresa() {

    var idaluno = $("#form-modal_aluno #idaluno").val();
    var txaluno = $("#form-modal_aluno #txaluno").val();
    var txcpf = $("#form-modal_aluno #txcpf").val();
    var txemail = $("#form-modal_aluno #txemail").val();
    var txtelefone = $("#form-modal_aluno #txtelefone").val();
    var idespecialidade = $("#form-modal_aluno #idespecialidade").val();
    var idcor = $("#form-modal_aluno #idcor").val();
    var idcidade = $("#form-modal_aluno #idcidade").val();
    var txempresa = $("#form-modal_aluno #txempresa").val();
    var txobs = $("#form-modal_aluno #txobs").val();
    var txredes = $("#form-modal_aluno #txredes").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Alunos/IncluirConcluir',
        data: { id: idaluno, nome: txaluno, cpf: txcpf, email: txemail.toString(), telefone: txtelefone.toString(), especialidade: idespecialidade, cidade: idcidade, cor: idcor, empresa: txempresa, obs: txobs, redes: txredes.toString() },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            if ($("#modal2").is(":visible")) {
                window.setTimeout(function () {
                    $('#modal2.modal').modal('hide');
                }, 1000);
            } else {

                window.setTimeout(function () {
                    $('#modal1.modal').modal('hide');
                }, 1000);

                AlunoPesquisar();
            }

        }
    });

}

function addEmail() {
    $('#form-modal-email').validationEngine('attach');
    if ($('#form-modal-email').validationEngine('validate')) {
        var temp = $("#tempemail").val();
        var cont = $("#listemail li").length;
        var txt = "";
        $("#listemail").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEmail(" + cont + ")'></i><span>" + temp + "</span></li>");
        $("#tempemail").val("");
        if ($("#txemail").val() === "") { txt = temp; } else { txt = $("#txemail").val() + "," + temp }
        $("#txemail").val(txt);
    };
}

function removeEmail(i) {
    var arr = $("#txemail").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x !== i) { txt = txt + "," + arr[x]; } }
    $("#txemail").val(txt.slice(1));
    $("#listemail").empty()
    if ($("#txemail").val() != "") {
        var arr = $("#txemail").val().split(",");
        for (x = 0; x < arr.length; x++) {
            $("#listemail").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEmail(" + x + ")'></i><span>" + arr[x] + "</span></li>");
        }
    }
}

function addTelefone() {
    $('#form-modal-telefone').validationEngine('attach');
    if ($('#form-modal-telefone').validationEngine('validate')) {
        var whatsapp = ($("#flwhatsapp").is(":checked") ? 1 : 0);
        var temp = $("#temptelefone").val();
        var cont = $("#listtelefone li").length;
        var txt = "";
        $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + cont + ")'></i><span>" + temp + (whatsapp == 1 ? " <i class='fa fa-whatsapp'></i> " : "") + "</span></li>");
        $("#temptelefone").val("");
        if ($("#txtelefone").val() == "") { txt = whatsapp + "|" +  temp; } else { txt = $("#txtelefone").val() + "," + whatsapp + "|" + temp }
        $("#txtelefone").val(txt);
        $('#flwhatsapp').prop('checked', false);
        $('#whatsapp_label').removeClass("verde_escuro")
    };
}

function removeTelefone(i) {
    var arr = $("#txtelefone").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txtelefone").val(txt.slice(1));
    $("#listtelefone").empty()
    if ($("#txtelefone").val() != "") {
        var arr = $("#txtelefone").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + x + ")'></i><span>" + arrT[1] + (arrT[0] == 1 ? " <i class='fa fa-whatsapp'></i> " : "") + "</span></li>");
        }
    }
}

function whatsapp(o) {
    i = $("#flwhatsapp").is(":checked");
    if (i) {
        $('#flwhatsapp').prop('checked', false);
        $('#whatsapp_label').removeClass("verde_escuro")
    } else {
        $('#flwhatsapp').prop('checked', true);
        $('#whatsapp_label').addClass("verde_escuro")
    }

}