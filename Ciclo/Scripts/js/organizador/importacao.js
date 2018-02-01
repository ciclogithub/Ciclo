function enviarArquivo(tp) {
    var arquivo = $("#txArquivo").val();
    var ext = arquivo.replace(/^.*\./, '').toLowerCase();
    if (arquivo == "") {
        swal({ title: "Selecione um arquivo", type: "error", timer: 3000 })
    } else {
        if ((ext != "xls") && (ext != "xlsx")) {
            swal({ title: "Arquivo em formato incorreto", type: "error", timer: 3000 })
        } else {
            $("#processando").html("Aguarde, processando arquivo");
            var form = $('#form-modal_importar')[0];
            var data = new FormData(form);
            if (tp == 1) { var url = "/Painel/Empresas/ImportarConcluir"; }
            if (tp == 2) { var url = "/Painel/Alunos/ImportarConcluir"; }
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                processData: false,
                contentType: false,
                cache: false,
                dataType: "json",
                traditional: true,
                success: function (data) {                          
                    $("#processando").html("Importação finalizada com sucesso!<br><a href='#' onClick='Relatorio()'>Clique aqui para ver o relatório final.</a>");
                }
            });
        }
    }
}

function Relatorio() {
    Modal2("/Painel/Painel/Relatorio", 0, "Relatório", "");
}