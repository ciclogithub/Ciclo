$(document).ready(function () {

    $("#pesquisa_aluno").click(function () {
        AlunoPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirAluno();
        };
    });

});

function AlunoPesquisar() {
    window.location = "/Painel/Alunos/?aluno=" + $("#aluno").val();
}

function Alunos(id) {
    Modal("/Painel/Alunos/Incluir", id, "Alunos");
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

    if (cont == 0) {
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

    if (ids != "") {
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

    $.ajax({
        type: "POST",
        url: '/Painel/Alunos/IncluirConcluir',
        data: { id: idaluno, nome: txaluno, cpf: txcpf },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

            AlunoPesquisar();

        }
    });

}