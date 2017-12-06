$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#pesquisa_instrutor").click(function () {
        InstrutorPesquisar();
    });

    $("#incluir_btn_instrutor").click(function () {
        $('#form-modal_instrutor').validationEngine('attach');
        if ($('#form-modal_instrutor').validationEngine('validate')) {
            ValidaInstrutorInclusao();
        };
    });

});

function InstrutoresTodos() {
    window.location = "/Painel/Instrutores/?pagina=1&filtro=";
}

function InstrutorPesquisar() {
    window.location = "/Painel/Instrutores/?filtro=" + $("#filtro_pesquisa").val();
}

function Instrutores(id) {
    Modal("/Painel/Instrutores/Incluir", id, "Instrutores", "");
}

function InstrutorAlterar() {

    cont = 0;
    val = 0;
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            cont++;
            val = $(this).val();
        }
    });

    if (cont === 0) {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    } else {
        if (cont > 1) {
            swal({ title: "Selecione somente 1 registro para alterar", type: "error", timer: 3000 });
        } else {
            Instrutores(val);
        }
    }

}

function InstrutorExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val()
        }
    });

    var ids = ids.substring(1);

    if (ids !== "") {
        ValidaInstrutorExcluir(ids);
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function IncluirInstrutor() {

    var idinstrutor = $("#form-modal_instrutor #idinstrutor").val();
    var txinstrutor = $("#form-modal_instrutor #txinstrutor").val();
    var txemail = $("#form-modal_instrutor #txemail").val();
    var txtelefone = $("#form-modal_instrutor #txtelefone").val();
    var txdescritivo = $("#form-modal_instrutor #txdescritivo").val();

    var form = $('#form-modal_instrutor')[0];
    var data = new FormData(form);
    data.append("id", idinstrutor);
    data.append("nome", txinstrutor);
    data.append("email", txemail);
    data.append("telefone", txtelefone);
    data.append("descricao", txdescritivo);

    $.ajax({
        type: "POST",
        url: '/Painel/Instrutores/IncluirConcluir',
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        dataType: "json",
        traditional: true,
        success: function (json) {

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000,
            }).then((result) => {
                if (result.dismiss === 'timer') {
                    closeModal("InstrutorPesquisar()");
                }
                if (result.value) {
                    closeModal("InstrutorPesquisar()");
                }
            })

        }
    });

}

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#prev_img').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function ValidaInstrutorInclusao() {

    $.post("/Painel/Instrutores/VerificaInstrutor", { id: $("#form-modal_instrutor #idinstrutor").val(), nome: $("#form-modal_instrutor #txinstrutor").val() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Já existe um instrutor com o mesmo nome, confirma a gravação de um novo registro',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    IncluirInstrutor();
                }
            })
        } else {
            IncluirInstrutor();
        }
    });
}

function ValidaInstrutorExcluir(ids) {
    $.post("/Painel/Instrutores/VerificaInstrutorExcluir", { id: ids.toString() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Existem cursos vinculados a um dos instrutores selecionados, confirma a exclusão',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Instrutores", "instrutor", "InstrutorPesquisar()", ids);
                }
            })
        } else {
            swal({
                title: 'Confirma a exclusão do(s) registro(s)',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Instrutores", "instrutor", "InstrutorPesquisar()", ids);
                }
            })
        }
    });
}