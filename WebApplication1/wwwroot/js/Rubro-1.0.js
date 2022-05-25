
function CompletarTablaRubros() {
    VaciarFormulario();
    $('#RubroID').val(0)
    $.ajax({
        type: "POST",
        url: '../../Rubros/BuscarRubros',
        data: {},
        success: function (listadoRubros) {
            $("#tbody-rubros").empty();
            $.each(listadoRubros, function (index, rubro) {

                let claseEliminado = '';              
                let botones = '<button type="button" onclick="BuscarRubro(' + rubro.rubroID + ')" class="btn btn-outline-primary btn-sm" style="margin-right:5px">Editar</button>' +
                    '<button type="button" onclick="EliminarRubro(' + rubro.rubroID + ',1)" class="btn btn-outline-danger btn-sm">Eliminar</button>';

                if (rubro.eliminado) {
                    claseEliminado = 'table-danger';
                    botones = '<button type="button" onclick="EliminarRubro(' + rubro.rubroID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                }

                $("#tbody-rubros").append('<tr class='+claseEliminado+'>' +
                        '<td class="border-dark">' + rubro.descripcion + '</td>' +
                        '<td class="border-dark text-center">' + rubro.rubroID + '</td>' +
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
    $("#Titulo-Modal-Rubro").text("Nuevo Rubro");
    $("#RubroID").val(0);
    $("#exampleModal").modal("show");

}

function GuardaRubro() {
    $("#Error-RubroNombre").text("");
    let rubroID = $('#RubroID').val();
    let guardarrubro = $('#RubroNombre').val().trim();
    /*let guardarrubro2 = document.getElementById('RubroNombre').value;*/
    if (guardarrubro != "" && guardarrubro != null) {
        $.ajax({
            type: "POST",
            url: '../../Rubros/GuardaRubro',
            data: { RubroID: rubroID, Descripcion: guardarrubro },
            success: function (resultado) {
                if (resultado == 0) {
                    $("#exampleModal").modal('hide')
                    CompletarTablaRubros();
                }
                if (resultado == 2) {
                    $("#Error-RubroNombre").text("El Rubro ingresado ya existe");
                }
            },
            error: function (data) {
            }
        });
    }
    else {
        $("#Error-RubroNombre").text("Debe ingresar un Rubro");
    }
    
}


function BuscarRubro(rubroID) {
    $("#Titulo-Modal-Rubro").text("Editar Rubro");
    $("#RubroID").val(rubroID);
    $.ajax({
        type: "POST",
        url: '../../Rubros/BuscarRubro',
        data: { RubroID: rubroID },
        success: function (rubro) {
            $("#RubroNombre").val(rubro.descripcion);
            $("#exampleModal").modal("show");
        },
        error: function (data) {
        }
    });
}

function VaciarFormulario() {
    $("#RubroID").val(0);
    $("#RubroNombre").val('');
    $("#Error-RubroNombre").text("");
}

function EliminarRubro(rubroID,elimina) {
    $.ajax({
        type: "POST",
        url: '../../Rubros/EliminarRubro',
        data: { RubroID: rubroID, Elimina: elimina },
        success: function (rubro) {
            CompletarTablaRubros();
        },
        error: function (data) {
        }
    });
}