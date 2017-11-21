$(document).ready(function () {

    $('#selectallav').click(function () {
        val = $(this).prop('checked')
        $('#table_avaliacao tr th[scope=row]').find('input').each(function (index) {
            $(this).prop('checked', val);
        });
    })

    $("#pesquisa_curso").click(function () {
        CursoPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirCurso();
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
        $("#bgcor").removeClass()
        $("#bgcor").addClass("input-group-addon")
        $("#bgcor").addClass($(this).find(":selected").text().replace(" ", "_"));

    })

    $("#incluir_avaliacoes_btn").click(function () {       
        cont = 0;
        val = 0;
        if ($("#form-modal_avaliacoes input[name='ident']").length === 0) {
            alert("Todos os alunos já receberam o formulário de avaliação");
        } else {
            $("#form-modal_avaliacoes input[name='ident']").each(function () {
                if ($(this).is(":checked")) {
                    cont++;
                    val = $(this).val();
                }
            });

            if (cont === 0) {
                alert("Selecione pelo menos 1 registro");
            } else {
                IncluirAvaliacao()
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

function pagination(c) {
    var p = $("#page").val();
    var t = $("#totalpage").val();
    if (c === -1) {
        c = parseInt(p) - 1;
        if (c <= 0) { c = 1 }
        window.location = "/Painel/Cursos/?pagina="+c+"&curso=" + $("#curso").val();
    } else {
        if (c === 0) {
            c = parseInt(p) + 1;
            if (c > t) { c = t }
            window.location = "/Painel/Cursos/?pagina=" + c +"&curso=" + $("#curso").val();
        } else {
            window.location = "/Painel/Cursos/?pagina=" + c +"&curso=" + $("#curso").val();
        }
    }

}

function CursosTodos() {
    window.location = "/Painel/Cursos/?pagina=1&curso=";
}

function CursoPesquisar() {
    window.location = "/Painel/Cursos/?pagina=1&curso=" + $("#curso").val();
}

function Cursos(id) {
    Modal("/Painel/Cursos/Incluir", id, "Cursos", "");
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

    if (cont === 0) {
        alert("Selecione pelo menos 1 registro");
    } else {
        if (cont > 1) {
            alert("Selecione somente 1 registro para alterar");
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

    if (ids !== "") {
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
        alert("Selecione pelo menos 1 registro");
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
    var idcor = $("#idcor").val();
    var txidentificador = $("#txidentificador").val();

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
    data.append("cor", idcor);
    data.append("identificador", txidentificador);
    
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
                $('#modal1.modal').modal('hide');
            }, 1000);

            CursoPesquisar();

        }
    });

}

/* INSTRUTORES */

function CursoInstrutor(id) {
    Modal("/Painel/Cursos/Instrutores", id, "Cursos - Instrutores", "");
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
                $('#modal1.modal').modal('hide');
            }, 1000);

        }
    });

}

function BuscarInstrutores() {

    var str = "";
    $("#lstinstrutor > option").each(function () {
        str += "," + $(this).val();
    });
    if (str === "") { str = 0; } else { str = str.slice(1); }

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
    Modal("/Painel/Cursos/Alunos", id, "Cursos - Alunos", "");
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
                $('#modal1.modal').modal('hide');
            }, 1000);

        }
    });

}

function BuscarAlunos() {

    var str = "";
    $("#lstaluno > option").each(function () {
        str += "," + $(this).val();
    });
    if (str === "") { str = 0; } else { str = str.slice(1); }

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
    Modal("/Painel/Cursos/Datas", id, "Cursos - Datas", "ListaDatas");
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
            alert("Operação realizada com sucesso!");
            NovaData();
            ListaDatas(idcurso);
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
    if (id !== "") {
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

    if (id !== "") {
        if (confirm("Certeza que deseja excluir o(s) registro(s) selecionado(s)?")) {
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
    }
}

/* VALORES */

function CursoValor(id) {
    Modal("/Painel/Cursos/Valores", id, "Cursos - Valores", "ListaValores");
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
            alert("Operação realizada com sucesso!");
            NovoValor();
            ListaValores(idcurso);
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
    if (id !== "") {
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

    if (id !== "") {
        if (confirm("Certeza que deseja excluir o(s) registro(s) selecionado(s)?")) {
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
    }
}

/* AVALIAÇÃO */

function CursoAvaliacao(id) {
    Modal("/Painel/Cursos/Avaliacao", id, "Cursos - Avaliações", "");
}

function IncluirAvaliacao() {
    ids = "";
    $("#form-modal_avaliacoes input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (confirm("Certeza que deseja enviar o formulário de avaliação para o(s) registro(s) selecionado(s)?")) {
        $.ajax({
            type: "POST",
            url: "/Painel/Cursos/AvaliacaoConcluir",
            data: { ident: ids },
            dataType: "json",
            traditional: true,
            success: function () {
                alert("Operação realizada com sucesso!");

                window.setTimeout(function () {
                    $('#modal1.modal').modal('hide');
                }, 1000);
            }
        });
    }
}

function ShowHide(id) {
    if ($("#cel_" + id).hasClass("hide")) {
        $("#cel_" + id).removeClass("hide");
    } else {
        $("#cel_" + id).addClass("hide")
    }
}

/* FOLDER */

function CursoFolder(id) {
    alert("Em desenvolvimento")
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