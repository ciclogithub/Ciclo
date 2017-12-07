$(function () {
    
});


function confirmaSair() {
    swal({
        title: 'Deseja realmente sair do sistema',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            location.href = "/Aluno/Aluno/Sair";
        }
    });
}

function MudaPais(val) {
    if (val == "") {
        $("#dv_estado").html("<label class='control-label' for='idestado'>Estado</label><select id='idestado' name='idestado' class='form-control'><option value=''>-- Selecione o país --</option></select>");
    } else {
        $("#dv_estado").html("<label class='control-label' for='idestado'>Estado</label>Carregando lista ...");
        ListaEstados(val, 0)
    }
    $("#dv_cidade").html("<label class='control-label' for='idcidade'>Cidade</label><select id='idcidade' name='idcidade' class='form-control'><option value=''>-- Selecione o estado --</option></select>");
}

function MudaEstado(val) {
    if (val == "") {
        $("#dv_cidade").html("<label class='control-label' for='idcidade'>Cidade</label><select id='idcidade' name='idcidade' class='form-control'><option value=''>-- Selecione o estado --</option></select>");
    } else {
        $("#dv_cidade").html("<label class='control-label' for='idcidade'>Cidade</label>Carregando lista ...");
        ListaCidades(val, 0)
    }
}

function ListaCidades(estado, cidade) {
    $.ajax({
        url: "../Painel/Locais/ListaCidades",
        data: { id: estado },
        cache: false,
        type: "POST",
        success: function (data) {
            var temp = "";
            temp += "<label class='control-label' for='idcidade'>Cidade</label><select id='idcidade' name='idcidade' class='form-control'><option value=''>-- Selecione --</option>";
            for (var x = 0; x < data.length; x++) {
                temp += "<option value=" + data[x].idcidade;
                if (data[x].idcidade == cidade) { temp += " selected " }
                temp += ">" + data[x].txcidade + "</option>";
            }
            temp += "</select>";
            $("#dv_cidade").html(temp);
            $('select#idcidade').select2();
        },
        error: function (reponse) {
            $("#dv_cidade").html("<label class='control-label' for='idcidade'>Cidade</label>Não foi possível carregar a lista de cidades");
        }
    });
}

function ListaEstados(pais, estado) {
    $.ajax({
        url: "../Painel/Locais/ListaEstados",
        data: { id: pais },
        cache: false,
        type: "POST",
        success: function (data) {
            var temp = "";
            temp += "<label class='control-label' for='idestado'>Estado</label><select id='idestado' name='idestado' onchange='MudaEstado(this.value)' class='form-control'><option value=''>-- Selecione --</option>";
            for (var x = 0; x < data.length; x++) {
                temp += "<option value=" + data[x].idestado;
                if (data[x].idestado == estado) { temp += " selected " }
                temp += ">" + data[x].txestado + "</option>";
            }
            temp += "</select>";
            $("#dv_estado").html(temp);
            $('select#idestado').select2();
        },
        error: function (reponse) {
            $("#dv_estado").html("<label class='control-label' for='idestado'>Estado</label>Não foi possível carregar a lista de estados");
        }
    });
}
