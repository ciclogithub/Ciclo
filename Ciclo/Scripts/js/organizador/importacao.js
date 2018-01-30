function enviarArquivo() {
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
            $.ajax({
                type: "POST",
                url: '/Painel/Alunos/ImportarConcluir',
                data: data,
                processData: false,
                contentType: false,
                cache: false,
                dataType: "json",
                traditional: true,
                success: function() {                          
                    $("#processando").html("finalizado");
                }
            });
        }
    }
}