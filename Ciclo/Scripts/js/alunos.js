$(document).ready(function () {

    $("#pesquisa_aluno").click(function () {
        AlunoPesquisar();
    });

    $("#incluir_btn_aluno").click(function () {
        var err = false;
        //if ($("#txemail").val() === "") {
        //    $("#error_email").css("display", "block");
        //    err = true;
        //} else {
        //    $("#error_email").css("display", "none");
        //}
        //if ($("#txtelefone").val() === "") {
        //    $("#error_telefone").css("display", "block");
        //    err = true;
        //} else {
        //    $("#error_telefone").css("display", "none");
        //}   
        $('#form-modal_aluno').validationEngine('attach');
        if ($('#form-modal_aluno').validationEngine('validate')) {
            if (!err) { IncluirAluno(); }
        }
    });

    $("#idestado").on("change", function () {
        if ($(this).val() === "") {
            $("#dv_cidade").html("<label class='control-label' for='idcidade'>Cidade</label><select id='idcidade' name='idcidade' class='form-control'><option value=''>-- Selecione o estado --</option></select>");
        } else {
            $("#dv_cidade").html("Carregando lista ...");
            ListaCidades($(this).val(), 0);
        }
    });

    $("#idcor").on("change", function () {
        $("#bgcor").removeClass();
        $("#bgcor").addClass("input-group-addon");
        $("#bgcor").addClass($(this).find(":selected").text().replace(" ", "_"));

    });
    

    if ($("#idestado")) {
        if ($("#idestado").val() !== "") {
            ListaCidades($("#idestado").val(), $("#tempcidade").val());
        }
    }

});

function ListaCidades(estado, cidade) {
    $.ajax({
        url: "/Painel/Locais/ListaCidades",
        data: { id: estado },
        cache: false,
        type: "POST",
        success: function (data) {
            var temp = "";
            temp += "<label class='control-label' for='idcidade'>Cidade</label>";
            temp += "<select id='idcidade' name='idcidade' class='form-control validate[required]'><option value=''>-- Selecione --</option>";
            for (var x = 0; x < data.length; x++) {
                temp += "<option value=" + data[x].idcidade;
                if (data[x].idcidade === cidade) { temp += " selected "; }
                temp += ">" + data[x].txcidade + "</option>";
            }
            temp += "</select>";
            $("#dv_cidade").html(temp);
        },
        error: function (reponse) {
            $("#dv_cidade").html("Não foi possível carregar a lista de cidades");
        }
    });
}

function pagination(c) {
    var p = $("#page").val();
    var t = $("#totalpage").val();
    if (c === -1) {
        c = parseInt(p) - 1;
        if (c <= 0) { c = 1; }
        window.location = "/Painel/Alunos/?pagina=" + c + "&aluno=" + $("#aluno").val();
    } else {
        if (c === 0) {
            c = parseInt(p) + 1;
            if (c > t) { c = t; }
            window.location = "/Painel/Alunos/?pagina=" + c + "&aluno=" + $("#aluno").val();
        } else {
            window.location = "/Painel/Alunos/?pagina=" + c + "&aluno=" + $("#aluno").val();
        }
    }
}

function AlunosTodos() {
    window.location = "/Painel/Alunos/?pagina=1&aluno=";
}

function AlunoPesquisar() {
    window.location = "/Painel/Alunos/?aluno=" + $("#aluno").val();
}

function Alunos(id) {
    Modal("/Painel/Alunos/Incluir", id, "Alunos", "");
}

function AlunoAlterar() {

    cont = 0;
    val = 0;
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            cont++;
            val = $(this).val();
        }
    });

    if (cont === 0) {
        alert("Selecione pelo menos 1 registro");
    } else {
        if (cont > 1) {
            alert("Selecione somente 1 registro para alterar");
        } else {
            Alunos(val);
        }
    }

}

function AlunoExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids !== "") {
        if (confirm("Certeza que deseja excluir o(s) registro(s) selecionado(s)?")) {
            $.ajax({
                type: "POST",
                url: "/Painel/Alunos/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#aluno").val("");
                    AlunoPesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro");
    }
}

function IncluirAluno() {

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
        if ($("#txemail").val() === "") { txt = temp; } else { txt = $("#txemail").val() + "," + temp; }
        $("#txemail").val(txt);
    }
}

function removeEmail(i) {
    var arr = $("#txemail").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x !== i) { txt = txt + "," + arr[x]; } }
    $("#txemail").val(txt.slice(1));
    $("#listemail").empty();
    if ($("#txemail").val() !== "") {
        arr = $("#txemail").val().split(",");
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
        $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + cont + ")'></i><span>" + temp + (whatsapp === 1 ? " <i class='fa fa-whatsapp'></i> " : "") + "</span></li>");
        $("#temptelefone").val("");
        if ($("#txtelefone").val() === "") { txt = whatsapp + "|" + temp; } else { txt = $("#txtelefone").val() + "," + whatsapp + "|" + temp; }
        $("#txtelefone").val(txt);
        $('#flwhatsapp').prop('checked', false);
        $('#whatsapp_label').removeClass("verde_escuro");
    }
}

function removeTelefone(i) {
    var arr = $("#txtelefone").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x !== i) { txt = txt + "," + arr[x]; } }
    $("#txtelefone").val(txt.slice(1));
    $("#listtelefone").empty();
    if ($("#txtelefone").val() !== "") {
        arr = $("#txtelefone").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + x + ")'></i><span>" + arrT[1] + (arrT[0] === 1 ? " <i class='fa fa-whatsapp'></i> " : "") + "</span></li>");
        }
    }
}

function addRedes() {
    $('#form-modal-redes').validationEngine('attach');
    if ($('#form-modal-redes').validationEngine('validate')) {
        var temp = $("#tempredes").val();
        var idrede = $("#idredesocial").val();
        var rede = $("#idredesocial option:selected").attr("name");
        var cont = $("#listredes li").length;
        var txt = "";
        $("#listredes").append("<li><i class='glyphicon glyphicon-trash' onclick='removeRedes(" + cont + ")'></i><span><i class='fa " + rede + "'></i> " + temp + "</span></li>");
        $("#tempredes").val("");
        if ($("#txredes").val() === "") { txt = idrede + "|" + temp + "|" + rede; } else { txt = $("#txredes").val() + "," + idrede + "|" + temp + "|" + rede; }
        $("#txredes").val(txt);
    }
}

function removeRedes(i) {
    var arr = $("#txredes").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x !== i) { txt = txt + "," + arr[x]; } }
    $("#txredes").val(txt.slice(1));
    $("#listredes").empty();
    if ($("#txredes").val() !== "") {
        arr = $("#txredes").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listredes").append("<li><i class='glyphicon glyphicon-trash' onclick='removeRedes(" + x + ")'></i><span><i class='fa " + arrT[2] + "'></i>" + arrT[1] + "</span></li>");
        }
    }
}

function whatsapp(o) {
    i = $("#flwhatsapp").is(":checked");
    if (i) {
        $('#flwhatsapp').prop('checked', false);
        $('#whatsapp_label').removeClass("verde_escuro");
    } else {
        $('#flwhatsapp').prop('checked', true);
        $('#whatsapp_label').addClass("verde_escuro");
    }

}