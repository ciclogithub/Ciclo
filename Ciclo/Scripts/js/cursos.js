$(document).ready(function () {

    $("#pesquisa_curso").click(function () {
        CursoPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirCurso();
        };
    });

});

function CursoPesquisar() {
    window.location = "/Painel/Cursos/?curso=" + $("#curso").val();
}

function Cursos(id) {
    Modal("/Painel/Cursos/Incluir", id, "Cursos");
}

function CursoAlterar() {

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
            Temas(val);
        }
    }

}

function CursoExcluir() {

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
                url: "/Painel/Cursos/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#tema").val("");
                    TemaPesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirCurso() {

    var idcurso = $("#idcurso").val();
    var txcurso = $("#txcurso").val();
    var idtema = $("#idtema").val();
    var idcategoria = $("#idcategoria").val();
    var idlocal = $("#idlocal").val();
    var txlocal = $("#txlocal").val();
    var nrminimo = $("#nrminimo").val();
    var nrmaximo = $("#nrmaximo").val();
    var txcargahoraria = $("#txcargahoraria").val();
    var txdescritivo = $("#txdescritivo").val();

    var form = $('#form-modal')[0];
    var data = new FormData(form);
    data.append("id", idcurso);
    data.append("nome_curso", txcurso);
    data.append("tema", idtema);
    data.append("categoria", idcategoria);
    data.append("codlocal", idlocal);
    data.append("local", txlocal);
    data.append("minimo", nrminimo);
    data.append("maximo", nrmaximo);
    data.append("cargahoraria", txcargahoraria);
    data.append("descricao", txdescritivo);

    $.ajax({
        type: "POST",
        url: '/Painel/Cursos/IncluirConcluir',
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

            CursoPesquisar();

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
