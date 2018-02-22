$(document).ready(function () {

    $("#pesquisa_inscricao").click(function () {
        InscricaoPesquisar();
    });

});

function InscricaoRecusar(id) {
    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });
    var ids = ids.substring(1);
    if (ids != "") {
        Modal("/Painel/Inscricoes/Recusar", ids, "Recusar Inscrições", "");
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function InscricaoConfirmar(id) {
    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });
    var ids = ids.substring(1);
    if (ids != "") {
        Modal("/Painel/Inscricoes/Confirmar", ids, "Confirmar Inscrições", "");
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function InscricaoPesquisar() {
    window.location = "/Painel/Inscricoes/?filtro=" + $("#filtro_pesquisa").val() + "&status=" + $("#filtro_status").val();
}


function DadosAluno(id) {
    Modal("/Painel/Inscricoes/Dados", id, "Dados do Aluno", "");
}