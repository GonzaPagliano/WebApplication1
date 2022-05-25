

function CompletarTablaSubRubros() {
    VaciarFormulario();
    $('#SubRubroID').val(0)
    $.ajax({
        type: "POST",
        url: '../../SubRubros/BuscarSubRubros',
        data: { },
        success: function (listadosubrubrosmostrar) {
            $("#tbody-subrubros").empty();
            $.each(listadosubrubrosmostrar, function (index, subrubro) {

                let claseEliminado = '';
                let botones = '<button type="button" onclick="BuscarSubRubro(' + subrubro.subRubroID + ')" class="btn btn-outline-primary btn-sm" style="margin-right:5px">Editar</button>' +
                    '<button type="button" onclick="EliminarSubRubro(' + subrubro.subRubroID + ',1)" class="btn btn-outline-danger btn-sm">Eliminar</button>';

                if (subrubro.eliminado) {
                    claseEliminado = 'table-danger';
                    botones = '<button type="button" onclick="EliminarSubRubro(' + subrubro.subRubroID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                }

                $("#tbody-subrubros").append('<tr class=' + claseEliminado + '>' +
                    '<td class="border-dark">' + subrubro.descripcion + '</td>' +
                    '<td class="border-dark">' + subrubro.rubroNombre + '</td>' +
                    '<td class="text-center border-dark">' +
                    botones +
                    '</td>' +
                    '</tr>');
            });
        },
        error: function (data) {
        }
    });
}


function AbrirModal() {
    $("#Titulo-Modal-SubRubro").text("Nuevo SubRubro")
    $("#SubRubroID").val(0);
    $("#RubroID").val(0);
    $("#exampleModal").modal("show");
    

}


function VaciarFormulario() {
    $("#SubRubroID").val(0);
    $("#RubroID").val(0);
    $("#SubRubroNombre").val('');
    $("#Error-SubRubroNombre").text("");
}



function GuardaSubRubro() {
    $("#Error-SubRubroNombre").text("");
    let subrubroID = $('#SubRubroID').val();
    let guardarsubrubro = $('#SubRubroNombre').val().trim();
    let rubroID = $('#RubroID').val();
    /*let guardarrubro2 = document.getElementById('RubroNombre').value;*/
    if (guardarsubrubro != "" && guardarsubrubro != null) {
        $.ajax({
            type: "POST",
            url: '../../SubRubros/GuardaSubRubro',
            data: { SubRubroID: subrubroID, Descripcion: guardarsubrubro, RubroID: rubroID,},
            success: function (resultado) {
                if (resultado == 0) {
                    $("#exampleModal").modal('hide')
                    CompletarTablaSubRubros();
                }
                if (resultado == 2) {
                    $("#Error-SubRubroNombre").text("El SubRubro ingresado ya existe");
                }
            },
            error: function (data) {
            }
        });
    }
    else {
        $("#Error-SubRubroNombre").text("Debe ingresar un Subrubro");
    }

}



function BuscarSubRubro(subrubroID) {
    $("#Titulo-Modal-SubRubro").text("Editar SubRubro")
    $("#SubRubroID").val(subrubroID);
    $("#RubroID").val(subrubroID);
    $.ajax({
        type: "POST",
        url: '../../SubRubros/BuscarSubRubro',
        data: { SubRubroID: subrubroID },
        success: function (subrubro) {
            $("#SubRubroNombre").val(subrubro.descripcion);
            $("#RubroID").val(subrubro.rubroID);
            $("#exampleModal").modal("show");
        },
        error: function (data) {
        }
    });
}



function EliminarSubRubro(subrubroID, elimina) {
    $.ajax({
        type: "POST",
        url: '../../SubRubros/EliminarSubRubro',
        data: { SubRubroID: subrubroID, Elimina: elimina },
        success: function (subrubro) {
            CompletarTablaSubRubros();
        },
        error: function (data) {
        }
    });
}