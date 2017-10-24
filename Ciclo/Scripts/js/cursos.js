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

    $("#incluir_instrutores_btn").click(function () {
        $('#lstinstrutor option').prop('selected', true);
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirInstrutores();
        };
    });

    $("#incluir_alunos_btn").click(function () {
        $('#lstaluno option').prop('selected', true);
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirAlunos();
        };
    });
    
    $("#cmbinstrutor").click(function () { addItem($(this), "lstinstrutor") })
    $("#lstinstrutor").click(function () { RemoveItem($(this), "cmbinstrutor") })
    $("#cmbaluno").click(function () { addItem($(this), "lstaluno") })
    $("#lstaluno").click(function () { RemoveItem($(this), "cmbaluno") })

});

function addItem(o, field) {
    $(o).find('option:selected').each(function () {
        $('#' + field).append(new Option($(this).prop("text"), $(this).val(), false, false));
        $(this).remove();
    });
    sortSelectOptions('#' + field, false);
}

function RemoveItem(o, field) {
    $(o).find('option:selected').each(function () {
        $('#' + field).append(new Option($(this).prop("text"), $(this).val(), false, false));
        $(this).remove();
    });
    sortSelectOptions('#' + field, false);
}

function sortSelectOptions(selector, skip_first) {
    var options = (skip_first) ? $(selector + ' option:not(:first)') : $(selector + ' option');
    var arr = options.map(function (_, o) { return { t: $(o).text(), v: o.value, s: $(o).prop('selected') }; }).get();
    arr.sort(function (o1, o2) {
        var t1 = o1.t.toLowerCase(), t2 = o2.t.toLowerCase();
        return t1 > t2 ? 1 : t1 < t2 ? -1 : 0;
    });
    options.each(function (i, o) {
        o.value = arr[i].v;
        $(o).text(arr[i].t);
        if (arr[i].s) {
            $(o).attr('selected', 'selected').prop('selected', true);
        } else {
            $(o).removeAttr('selected');
            $(o).prop('selected', false);
        }
    });
}

/* CURSOS */

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
            Cursos(val);
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
                    CursoPesquisar();
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
    var flgratuito = $("#flgratuito").prop('checked');

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
    data.append("gratuito", flgratuito);

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

/* INSTRUTORES */

function CursoInstrutor(id) {
    Modal("/Painel/Cursos/Instrutores", id, "Cursos - Instrutores");
}

function IncluirInstrutores() {

    var idcurso = $("#idcurso").val();    
    var lstinstrutor = $("#lstinstrutor").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Cursos/IncluirInstrutores',
        data: { id: idcurso, instrutores: lstinstrutor.toString() },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

        }
    });

}

/* ALUNOS */

function CursoAluno(id) {
    Modal("/Painel/Cursos/Alunos", id, "Cursos - Alunos");
}

function IncluirAlunos() {

    var idcurso = $("#idcurso").val();
    var lstaluno = $("#lstaluno").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Cursos/IncluirAlunos',
        data: { id: idcurso, alunos: lstaluno.toString() },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

        }
    });

}

/* DATAS */

function CursoData(id) {

}

/* VALORES */

function CursoValor(id) {

}

/* FOLDER */

function CursoFolder(id) {

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
