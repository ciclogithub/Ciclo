$(document).ready(function () {

$('select').not('.no-js').select2();

$('#selectallav').click(function () {
    val = $(this).prop('checked');
    $('#table_avaliacao tr th[scope=row]').find('input').each(function (index) {
        $(this).prop('checked', val);
    });
});

$("#sugEspecialidade").click(function () {
    swal({
        title: 'Sugestão de especialidade',
        input: 'text',
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonText: 'Enviar',
        showLoaderOnConfirm: true,
        inputPlaceholder: 'Especialidade',
        inputValidator: (value) => {
            if (!value) {
                return 'Preencha o campo corretamente';
            }
        },
        allowOutsideClick: false
    }).then((result) => {
        if (result.value) {
            valor = result.value;
            $.ajax({
                type: "POST",
                url: "Cursos/SugestaoEspecialidade/",
                data: { sugestao: valor },
                dataType: "json",
                traditional: true,
                async: false,
                success: function (retorno) {
                    swal({ title: 'Obrigado pela sugestão, iremos analisar e em breve entraremos em contato.', type: "success", timer: 3000 });
                }
            });
        }
    })
});

    $("#pesquisa_curso").click(function () {
        CursoPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal_curso').validationEngine('attach');
        if ($('#form-modal_curso').validationEngine('validate')) {
            ValidaCursoInclusao();
        }
    });

    $("#incluir_instrutores_btn").click(function () {
        $('#lstinstrutor option').prop('selected', true);
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirInstrutores();
        }
    });

    $("#incluir_alunos_btn").click(function () {
        $('#lstaluno option').prop('selected', true);
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirAlunos();
        }
    });

    $("#incluir_datas_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirDatas();
        }
    });

    $("#incluir_valores_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirValores();
        }
    });

    $("#idcor").on("change", function () {
        $("#bgcor").removeClass();
        $("#bgcor").addClass("input-group-addon");
        $("#bgcor").addClass($(this).find(":selected").text().replace(" ", "_"));
    });

    $("#incluir_avaliacoes_btn").click(function () {       
        cont = 0;
        val = 0;
        if ($("#form-modal_avaliacoes input[name='ident']").length == 0) {
            swal({ title: "Todos os alunos já receberam o formulário de avaliação", type: "info", timer: 3000 });
        } else {
            $("#form-modal_avaliacoes input[name='ident']").each(function () {
                if ($(this).is(":checked")) {
                    cont++;
                    val = $(this).val();
                }
            });

            if (cont == 0) {
                swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
            } else {
                IncluirAvaliacao();
            }
        }
    });

    
    $("#cmbinstrutor").click(function () { addItem($(this), "lstinstrutor"); });
    $("#lstinstrutor").click(function () { RemoveItem($(this), "cmbinstrutor"); BuscarInstrutores(); });
    $("#cmbaluno").click(function () { addItem($(this), "lstaluno"); });
    $("#lstaluno").click(function () { RemoveItem($(this), "cmbaluno"); BuscarAlunos(); });
    $("#novo_datas_btn").click(function () { NovaData(); });
    $("#novo_valores_btn").click(function () { NovoValor(); });
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
    var options = skip_first ? $(selector + ' option:not(:first)') : $(selector + ' option');
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

function CursosTodos() {
    window.location = "/Painel/Cursos/?pagina=1&filtro=";
}

function CursoPesquisar() {
    val = "";
    if ($("#filtro_pesquisa").length > 0) { val = $("#filtro_pesquisa").val() }
    window.location = "/Painel/Cursos/?pagina=1&filtro=" + val;
}

function Cursos(id) {
    Modal("/Painel/Cursos/Incluir", id, "Eventos", "");
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
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    } else {
        if (cont > 1) {
            swal({ title: "Selecione somente 1 registro para alterar", type: "error", timer: 3000 });
        } else {
            Cursos(val);
        }
    }

}

function CursoExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        ValidaCursoExcluir(ids);
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function IncluirCurso() {

    var idcurso = $("#form-modal_curso #idcurso").val();
    var txcurso = $("#form-modal_curso #txcurso").val();
    var idtema = $("#form-modal_curso #idtema").val();
    var idcategoria = $("#form-modal_curso #idcategoria").val();
    var idlocal = $("#form-modal_curso #idlocal").val();
    var txlocal = $("#form-modal_curso #txlocal").val();
    var nrminimo = $("#form-modal_curso #nrminimo").val();
    var nrmaximo = $("#form-modal_curso #nrmaximo").val();
    var txcargahoraria = $("#form-modal_curso #txcargahoraria").val();
    var txdescritivo = $("#form-modal_curso #txdescritivo").val();
    var flgratuito = $("#form-modal_curso #flgratuito").prop('checked');
    var idcor = $("#form-modal_curso #idcor").val();
    var txidentificador = $("#form-modal_curso #txidentificador").val();
    var txmercados = $("#form-modal_curso #txmercados").val();
    var txespecialidades = $("#form-modal_curso #txespecialidades").val();
    
    var form = $('#form-modal_curso')[0];
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
    data.append("cor", idcor);
    data.append("identificador", txidentificador);
    data.append("mercados", txmercados);
    data.append("especialidades", txespecialidades);
    
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

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    closeModal("CursoPesquisar()");
                }
                if (result.value) {
                    closeModal("CursoPesquisar()");
                }
            });

        }
    });
}

function ValidaCursoInclusao() {

    $.post("/Painel/Cursos/VerificaCurso", { id: $("#form-modal_curso #idcurso").val(), nome: $("#form-modal_curso #txcurso").val(), identificador: $("#form-modal_curso #txidentificador").val() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Já existe um evento com o mesmo nome, confirma a gravação de um novo registro',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    IncluirCurso();
                }
            });
        } else {
            if (data == 2) {
                swal({
                    title: 'Já existe um evento com o mesmo identificador, confirma a gravação de um novo registro',
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonText: 'Sim',
                    cancelButtonText: 'Não'
                }).then((result) => {
                    if (result.value) {
                        IncluirCurso();
                    }
                });
            } else {
                IncluirCurso();
            }
        }
    });
}

function ValidaCursoExcluir(ids) {
    $.post("/Painel/Cursos/VerificaCursoExcluir", { id: ids.toString() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Existem alunos vinculados a um dos eventos selecionados, confirma a exclusão',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Cursos", "curso", "CursoPesquisar()", ids);
                }
            });
        } else {
            swal({
                title: 'Confirma a exclusão do(s) registro(s)',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Cursos", "curso", "CursoPesquisar()", ids);
                }
            });
        }
    });
}

/* INSTRUTORES */

function CursoInstrutor(id) {
    Modal("/Painel/Cursos/Instrutores", id, "Eventos - Instrutores", "");
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

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    closeModal("");
                }
                if (result.value) {
                    closeModal("");
                }
            });

        }
    });

}

function BuscarInstrutores() {

    var str = "";
    $("#lstinstrutor > option").each(function () {
        str += "," + $(this).val();
    });
    if (str == "") { str = 0; } else { str = str.slice(1); }

    $("#cmbinstrutor").empty();
    $("#cmbinstrutor").append($('<option>', {
        value: 0,
        text: "Carregando lista ..."
    }));
    $.ajax({
        url: "/Painel/Cursos/ListaInstrutores",
        data: { instrutor: $("#txinstrutor").val(), lista: str.toString() },
        cache: false,
        type: "POST",
        success: function (data) {
            $("#cmbinstrutor").empty();
            for (var x = 0; x < data.length; x++) {
                $('#cmbinstrutor').append($('<option>', {
                    value: data[x].idinstrutor,
                    text: data[x].txinstrutor
                }));
            }
        },
        error: function (reponse) {
            $("#cmbinstrutor").empty();
            $("#cmbinstrutor").append($('<option>', {
                value: 0,
                text: "Não foi possível carregar a lista de instrutores"
            }));
        }
    });
    $("#txinstrutor").val("");
}

function addInstrutor() {
    Modal2("/Painel/Instrutores/Incluir", 0, "Instrutores", "BuscarInstrutores");
}

/* ALUNOS */

function CursoAluno(id) {
    Modal("/Painel/Cursos/Alunos", id, "Eventos - Alunos", "");
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

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    closeModal("");
                }
                if (result.value) {
                    closeModal("");
                }
            });

        }
    });

}

function BuscarAlunos() {

    var str = "";
    $("#lstaluno > option").each(function () {
        str += "," + $(this).val();
    });
    if (str == "") { str = 0; } else { str = str.slice(1); }

    $("#cmbaluno").empty();
    $("#cmbaluno").append($('<option>', {
        value: 0,
        text: "Carregando lista ..."
    }));
    $.ajax({
        url: "/Painel/Cursos/ListaAlunos",
        data: { aluno: $("#txaluno").val(), lista: str.toString() },
        cache: false,
        type: "POST",
        success: function (data) {
            $("#cmbaluno").empty();
            for (var x = 0; x < data.length; x++) {
                $('#cmbaluno').append($('<option>', {
                    value: data[x].idaluno,
                    text: data[x].txaluno
                }));
            }
        },
        error: function (reponse) {
            $("#cmbaluno").empty();
            $("#cmbaluno").append($('<option>', {
                value: 0,
                text: "Não foi possível carregar a lista de alunos"
            }));
        }
    });
    $("#txaluno").val("");
}

function addAluno() {
    Modal2("/Painel/Alunos/Incluir", 0, "Alunos", "BuscarAlunos");
}

/* DATAS */

function CursoData(id) {
    Modal("/Painel/Cursos/Datas", id, "Eventos - Datas", "ListaDatas");
}

function NovaData() {
    var temp = $("#idcurso").val();
    $("#iddatacurso").val("");
    $("#form-modal")[0].reset();
    $("#idcurso").val(temp);
}

function IncluirDatas() {

    var iddatacurso = $("#iddatacurso").val();
    var idcurso = $("#idcurso").val();
    var dtcurso = $("#dtcurso").val();
    var hrinicio = $("#hrinicio").val();
    var hrfim = $("#hrfim").val();
    var txdescritivo = $("#txdescritivo").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Cursos/IncluirDatas',
        data: { iddata: iddatacurso, idcurso: idcurso, data: dtcurso, horaini: hrinicio, horafim: hrfim, descricao: txdescritivo },
        dataType: "json",
        traditional: true,
        success: function (json) {

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    NovaData();
                    ListaDatas(idcurso);
                }
                if (result.value) {
                    NovaData();
                    ListaDatas(idcurso);
                }
            });
        }
    });

}

function ListaDatas(id) {
    $.ajax({
        url: "/Painel/Cursos/ListaDatas",
        data: { id: id },
        cache: false,
        type: "POST",
        success: function (data) {
            var temp = "";
            for (var x = 0; x < data.length; x++) {
                temp += "<div class='row list'>";
                temp += "<div class='col-xs-9 col-sm-9 col-md-9 col-lg-9'>" + data[x].dtcurso + " - " + data[x].hrinicial + " às " + data[x].hrfinal + "</div>";
                temp += "<div class='col-xs-3 col-sm-3 col-md-3 col-lg-3 text-right'>";
                temp += "<i onClick='AlterarData(" + data[x].iddatacurso  + ")' class='glyphicon glyphicon-pencil' title= 'Alterar' ></i >";
                temp += "<i onClick='ExcluirData(" + data[x].iddatacurso + ")' class='glyphicon glyphicon-trash' title='Excluir'></i></div>";
                temp += "</div>";
            }
            $("#list_datas").html(temp);
        },
        error: function (reponse) {
            $("#list_datas").html("Não foi possível carregar a lista de datas");
        }
    });
}

function AlterarData(id) {
    if (id != "") {
        $.ajax({
            type: "POST",
            url: "/Painel/Cursos/AlterarData",
            data: { ident: id },
            dataType: "json",
            traditional: true,
            success: function (data) {
                $("#iddatacurso").val(data.iddatacurso);
                $("#dtcurso").val(data.dtcurso);
                $("#hrinicio").val(data.hrinicial);
                $("#hrfim").val(data.hrfinal);
                $("#txdescritivo").val(data.txobs);
            }
        });
    }
}

function ExcluirData(id) {

    if (id != "") {
        swal({
            title: 'Confirma a exclusão do(s) registro(s)',
            type: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: "/Painel/Cursos/ExcluirData",
                    data: { ident: id },
                    dataType: "json",
                    traditional: true,
                    success: function () {
                        ListaDatas($("#idcurso").val());
                    }
                });
            }
        });
    }
}

/* VALORES */

function CursoValor(id) {
    Modal("/Painel/Cursos/Valores", id, "Eventos - Valores", "ListaValores");
}

function NovoValor() {
    var temp = $("#idcurso").val();
    $("#idvalorcurso").val("");
    $("#form-modal")[0].reset();
    $("#idcurso").val(temp);
}

function IncluirValores() {

    var idvalorcurso = $("#idvalorcurso").val();
    var idcurso = $("#idcurso").val();
    var nrvalor = $("#nrvalor").val().replace(".", "");
    var dtvalor = $("#dtvalor").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Cursos/IncluirValores',
        data: { idvalor: idvalorcurso, idcurso: idcurso, valor: nrvalor, data: dtvalor },
        dataType: "json",
        traditional: true,
        success: function (json) {

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    NovoValor();
                    ListaValores(idcurso);
                }
                if (result.value) {
                    NovoValor();
                    ListaValores(idcurso);
                }
            });
        }
    });

}

function ListaValores(id) {
    $.ajax({
        url: "/Painel/Cursos/ListaValores",
        data: { id: id },
        cache: false,
        type: "POST",
        success: function (data) {
            var temp = "";
            for (var x = 0; x < data.length; x++) {
                temp += "<div class='row list'>";
                temp += "<div class='col-xs-9 col-sm-9 col-md-9 col-lg-9'>Até " + data[x].dtvalor + " - Valor: R$ " + data[x].valor + "</div>";
                temp += "<div class='col-xs-3 col-sm-3 col-md-3 col-lg-3 text-right'>";
                temp += "<i onClick='AlterarValor(" + data[x].idvalorcurso + ")' class='glyphicon glyphicon-pencil' title= 'Alterar' ></i >";
                temp += "<i onClick='ExcluirValor(" + data[x].idvalorcurso + ")' class='glyphicon glyphicon-trash' title='Excluir'></i></div>";
                temp += "</div>";
            }
            $("#list_valores").html(temp);
        },
        error: function (reponse) {
            $("#list_valores").html("Não foi possível carregar a lista de valores");
        }
    });
}

function AlterarValor(id) {
    if (id != "") {
        $.ajax({
            type: "POST",
            url: "/Painel/Cursos/AlterarValor",
            data: { ident: id },
            dataType: "json",
            traditional: true,
            success: function (data) {
                $("#idvalorcurso").val(data.idvalorcurso);
                $("#nrvalor").val(data.valor);
                $("#dtvalor").val(data.dtvalor);
            }
        });
    }
}

function ExcluirValor(id) {

    if (id != "") {
        swal({
            title: 'Confirma a exclusão do(s) registro(s)',
            type: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: "/Painel/Cursos/ExcluirValor",
                    data: { ident: id },
                    dataType: "json",
                    traditional: true,
                    success: function () {
                        ListaValores($("#idcurso").val());
                    }
                });
            }
        });
    }
}

/* AVALIAÇÃO */

function CursoAvaliacao(id) {
    Modal("/Painel/Cursos/Avaliacao", id, "Eventos - Avaliações", "");
}

function IncluirAvaliacao() {
    ids = "";
    $("#form-modal_avaliacoes input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    swal({
        title: 'Confirma o envio do formulário de avaliação para o(s) registro(s) selecionado(s)',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Painel/Cursos/AvaliacaoConcluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    swal({
                        title: 'Operação realizada com sucesso!',
                        type: 'success',
                        confirmButtonText: 'Fechar',
                        timer: 3000
                    }).then((result) => {
                        if (result.dismiss == 'timer') {
                            closeModal("");
                        }
                        if (result.value) {
                            closeModal("");
                        }
                    });
                }
            });
        }
    });
}

function ShowHide(id) {
    if ($("#cel_" + id).hasClass("hide")) {
        $("#cel_" + id).removeClass("hide");
    } else {
        $("#cel_" + id).addClass("hide");
    }
}

/* LISTA DE ALUNOS */

function CursoListaAluno(id) {
    Modal("/Painel/Cursos/Lista", id, "Eventos - Lista de Alunos", "");
}


function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#prev_img').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}


function addTema() {
    Modal2("/Painel/Temas/Incluir", 0, "Temas", "ListaTemas");
}

function ListaTemas() {    
    $("#idtema").empty();
    $.ajax({
        url: "/Painel/Cursos/ListaTemas",
        cache: false,
        type: "POST",
        success: function (data) {
            $('#idtema').append($('<option>', {
                value: "",
                text: "-- Selecione --"
            }));
            for (var x = 0; x < data.length; x++) {
                $('#idtema').append($('<option>', {
                    value: data[x].idtema,
                    text: data[x].txtema
                }));
            }
        },
        error: function (reponse) {
            $('#idtema').append($('<option>', {
                value: 0,
                text: "Não foi possível carregar a lista de temas"
            }));
        }
    });
}

function addLocal() {
    Modal2("/Painel/Locais/Incluir", 0, "Locais", "ListaLocais");
}

function ListaLocais() {
    $("#idlocal").empty();
    $.ajax({
        url: "/Painel/Cursos/ListaLocais",
        cache: false,
        type: "POST",
        success: function (data) {
            $('#idlocal').append($('<option>', {
                value: "",
                text: "-- Selecione --"
            }));
            for (var x = 0; x < data.length; x++) {
                $('#idlocal').append($('<option>', {
                    value: data[x].idlocal,
                    text: data[x].txlocal
                }));
            }
        },
        error: function (reponse) {
            $('#idlocal').append($('<option>', {
                value: 0,
                text: "Não foi possível carregar a lista de locais"
            }));
        }
    });
}

function addMercado() {
    var idmercado = $("#idmercado").val();
    if (idmercado != "") {
        var mercado = $("#idmercado option:selected").html();
        if (!inArray("txmercados", idmercado + "|" + mercado)) {
            var cont = $("#listmercado li").length;
            var txt = "";
            $("#listmercado").append("<li><i class='glyphicon glyphicon-trash' onclick='removeMercados(" + cont + ")'></i><span>" + mercado + "</span></li>");
            if ($("#txmercados").val() == "") { txt = idmercado + "|" + mercado; } else { txt = $("#txmercados").val() + "," + idmercado + "|" + mercado; }
            $("#txmercados").val(txt);
        } else {
            alert("Já selecionado");
        }
    }
}

function removeMercados(i) {
    var arr = $("#txmercados").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txmercados").val(txt.slice(1));
    $("#listmercado").empty();
    if ($("#txmercados").val() != "") {
        arr = $("#txmercados").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listmercado").append("<li><i class='glyphicon glyphicon-trash' onclick='removeMercados(" + x + ")'></i><span>" + arrT[1] + "</span></li>");
        }
    }
}

function addEspecialidade() {
    var idespecialidade = $("#idespecialidade").val();
    if (idespecialidade != "") {
        var especialidade = $("#idespecialidade option:selected").html();
        if (!inArray("txespecialidades", idespecialidade + "|" + especialidade)) {
            var cont = $("#listespecialidade li").length;
            var txt = "";
            $("#listespecialidade").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEspecialidades(" + cont + ")'></i><span>" + especialidade + "</span></li>");
            if ($("#txespecialidades").val() == "") { txt = idespecialidade + "|" + especialidade; } else { txt = $("#txespecialidades").val() + "," + idespecialidade + "|" + especialidade; }
            $("#txespecialidades").val(txt);
        } else {
            alert("Já selecionado");
        }
    }
}

function removeEspecialidades(i) {
    var arr = $("#txespecialidades").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txespecialidades").val(txt.slice(1));
    $("#listespecialidade").empty();
    if ($("#txespecialidades").val() != "") {
        arr = $("#txespecialidades").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listespecialidade").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEspecialidades(" + x + ")'></i><span>" + arrT[1] + "</span></li>");
        }
    }
}

function inArray(campo, val) {
    arr = $("#" + campo).val().split(",");
    if ($.inArray(val, arr) < 0) { return false; } else { return true; }
}