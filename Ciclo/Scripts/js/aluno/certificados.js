$(document).ready(function () {

    $(".bg-menu").height($("#tg-main").height());

    $('select').not('.no-js').select2();

    $("#pesquisa_certificado").click(function () {
        CertificadoPesquisar();
    });

    $("#incluir_btn_certificado").click(function () {
        $('#form-modal_certificado').validationEngine('attach');
        if ($('#form-modal_certificado').validationEngine('validate')) {
            ValidaCertificadoInclusao();
        }
    });

});

function CertificadosTodos() {
    window.location = "/Aluno/Certificados/?pagina=1&filtro=";
}

function CertificadoPesquisar() {
    window.location = "/Aluno/Certificados/?filtro=" + $("#filtro_pesquisa").val();
}

function Certificados(id) {
    Modal("/Aluno/Certificados/Incluir", id, "Certificados", "");
}

function CertificadoAlterar() {

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
            Certificados(val);
        }
    }

}

function CertificadoExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        ValidaCertificadoExcluir(ids);
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function IncluirCertificado() {

    var idcertificado = $("#form-modal_certificado #idcertificado").val();
    var txcertificadora = $("#form-modal_certificado #txcertificadora").val();
    var txcurso = $("#form-modal_certificado #txcurso").val();
    var dtinicio = $("#form-modal_certificado #dtinicio").val();
    var dtfim = $("#form-modal_certificado #dtfim").val();
    var txinstrutor = $("#form-modal_certificado #txinstrutor").val();
    var nrnota = $("#form-modal_certificado #nrnota").val();

    $.ajax({
        type: "POST",
        url: '/Aluno/Certificados/IncluirConcluir',
        data: { id: idcertificado, certificadora: txcertificadora, curso: txcurso, data_inicio: dtinicio, data_fim: dtfim, instrutor: txinstrutor, nota: nrnota },
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
                    closeModal("CertificadoPesquisar()");
                }
                if (result.value) {
                    closeModal("CertificadoPesquisar()");
                }
            });

        }
    });

}


function ValidaCertificadoInclusao() {
    IncluirCertificado();
}

function ValidaCertificadoExcluir(ids) {    
    swal({
        title: 'Confirma a exclusão do(s) registro(s)',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    }).then((result) => {
        if (result.value) {
            confirmaExcluir("Certificados", "certificado", "CertificadoPesquisar()", ids);
        }
    });
}