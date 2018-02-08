$(document).ready(function () {

    cont = $("#cont_inscricoes").val();

    if (cont > 0) {
        swal({
            title: 'Existem ' + cont + ' novas solicitações de inscrições para eventos',
            type: 'info',
            showCancelButton: true,
            confirmButtonText: 'Ver todas',
            cancelButtonText: 'Fechar',
        }).then((result) => {
            if (result.value) {
                location.href = "Painel/Inscricoes";
            }
        });
    }  

});

function CursoInstrutor(id) {
    Modal("/Painel/Cursos/Instrutores", id, "Eventos - Instrutores", "");
}

function CursoAluno(id) {
    Modal("/Painel/Cursos/Alunos", id, "Eventos - Alunos", "");
}

function CursoData(id) {
    Modal("/Painel/Cursos/Datas", id, "Eventos - Datas", "ListaDatas");
}

function CursoValor(id) {
    Modal("/Painel/Cursos/Valores", id, "Eventos - Valores", "ListaValores");
}

function CursoAvaliacao(id) {
    Modal("/Painel/Cursos/Avaliacao", id, "Eventos - Avaliações", "");
}

function CursoListaAluno(id) {
    Modal("/Painel/Cursos/Lista", id, "Eventos - Lista de Alunos", "");
}