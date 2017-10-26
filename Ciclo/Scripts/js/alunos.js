$(document).ready(function () {

    $("#pesquisa_aluno").click(function () {
        AlunoPesquisar();
    });

    $("#incluir_btn").click(function () {
        var err = false;
        if ($("#txemail").val() === "") {
            $("#error_email").css("display", "block");
            err = true;
        } else {
            $("#error_email").css("display", "none");
        }
        if ($("#txtelefone").val() === "") {
            $("#error_telefone").css("display", "block");
            err = true;
        } else {
            $("#error_telefone").css("display", "none");
        }   
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            if (!err) { IncluirAluno(); }
        };
    });

});

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
        alert("Selecione pelo menos 1 registro")
    } else {
        if (cont > 1) {
            alert("Selecione somente 1 registro para alterar")
        } else {
            Alunos(val);
        }
    }

}

function AlunoExcluir() {

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
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirAluno() {

    var idaluno = $("#idaluno").val();
    var txaluno = $("#txaluno").val();
    var txcpf = $("#txcpf").val();
    var txemail = $("#txemail").val();
    var txtelefone = $("#txtelefone").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Alunos/IncluirConcluir',
        data: { id: idaluno, nome: txaluno, cpf: txcpf, email: txemail.toString(), telefone: txtelefone.toString() },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('#modal1.modal').modal('hide');
            }, 1000);

            AlunoPesquisar();

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
        var temp = $("#temptelefone").val();
        var cont = $("#listtelefone li").length;
        var txt = "";
        $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + cont + ")'></i><span>" + temp + "</span></li>");
        $("#temptelefone").val("");
        if ($("#txtelefone").val() == "") { txt = temp; } else { txt = $("#txtelefone").val() + "," + temp }
        $("#txtelefone").val(txt);
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
            $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + x + ")'></i><span>" + arr[x] + "</span></li>");
        }
    }
}