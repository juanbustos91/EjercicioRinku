// Definición de variables:
const url = "https://localhost:7168/api/Movimientos/"
const urlEmpleados = "https://localhost:7168/api/Empleados/"
const contenedor = document.querySelector('tbody')
const selectEmpleado = document.getElementById('selectEmpleado')
let resultados = ''
let listEmpleados = ''


const modalMovimientos = new bootstrap.Modal(document.getElementById('modalMovimientos'))
//const formMovimientos = document.querySelector('form')
const formMovimientos = document.getElementById('formMovimientos')
const id = document.getElementById('idMov')
const numEmpleado = document.getElementById('selectEmpleado')
const mes = document.getElementById('mes')
const cantEnt = document.getElementById('cantEnt')
let opcion = ''
let maxId = 0
let dataEmpResp = []


btnCrear.addEventListener('click', ()=>{

    id.value = maxId
    mes.value = ''
    cantEnt.value = ''

    opcion = 'crear'

    lstEmp()
    modalMovimientos.show()
})

// Traigo info de los empleados
fetch(urlEmpleados)
    .then(response => response.json())
    .then(data => copiarDatosEmp(data))
    .catch(error => console.log(error))


const copiarDatosEmp = (dataEmp) => {
    console.log(dataEmp)
    dataEmpResp = dataEmp
}

console.log(dataEmpResp)

function getNombreEmp(_idEmp){
    let name = ""
    dataEmpResp.forEach(e =>{
        if(_idEmp == e.id){
            name = e.nombre
        }
    })
    return name
}



// Función para mostrar resultados:
const mostrar = (movimientos) => {
    movimientos.forEach(mov =>{
        if(mov.id > maxId){
            maxId = mov.id
        }
        console.log(mov)

        resultados += `
            <tr>
                <td>${mov.id}</td>
                <td>${mov.numEmpleado}</td>
                <td>${getNombreEmp(mov.numEmpleado)}</td>
                <td>${definirMes(mov.mes)}</td>
                <td>${mov.cantidadEntregas}</td>
                <td>${mov.sueldoBruto}</td>
                <td>- ${mov.isr}</td>
                <td>- ${mov.isrAdicional}</td>
                <td>${mov.vales}</td>
                <td>${mov.sueldoNeto}</td>
                <td class="text-center"> <a class="btnEditar btn btn-primary">Editar</a> <a class="btnBorrar btn btn-danger">Borrar</a> </td>
            </tr>
            `        
    });

    maxId ++
    contenedor.innerHTML = resultados

}


// Función para cargar lista de empleados:
const lstEmp = (dataEmpResp) => {


    fetch(urlEmpleados)
    .then(response => response.json())
    .then(data => {
        listEmpleados = ''
        data.forEach(emp =>{
            console.log(emp)
    
            listEmpleados += `
                <option value="${emp.id}">${emp.nombre}</option>
                `
        });
    })
    .catch(error => console.log(error))

    selectEmpleado.innerHTML = listEmpleados

    console.log(listEmpleados)
    console.log(selectEmpleado)
    console.log(selectEmpleado.innerHTML)

}
lstEmp()





const definirMes = (numMes) =>{
    switch(numMes){
        case 1:
            return "Enero"
        case 2:
            return "Febrero"
        case 3:
            return "Marzo"
        case 4:
            return "Abril"
        case 5:
            return "Mayo"
        case 6:
            return "Junio"
        case 7:
            return "Julio"        
        case 8:
            return "Agosto"
        case 9:
            return "Septiembre"
        case 10:
            return "Ocubre"
        case 11:
            return "Noviembre"
        case 12:
            return "Diciembre"
    }
}

const definirMesStr = (StrMes) =>{
    switch(StrMes){
        case "Enero":
            return 1
        case "Febrero":
            return 2
        case "Marzo":
            return 3
        case "Abril":
            return 4
        case "Mayo":
            return 5
        case "Junio":
            return 6
        case "Julio":
            return 7
        case "Agosto":
            return 8
        case "Septiembre":
            return 9
        case "Ocubre":
            return 10
        case "Noviembre":
            return 11
        case "Diciembre":
            return 12
    }
}

//      Procedimiento Mostrar:
fetch(url)
    .then(response => response.json())
    .then(data => mostrar(data))
    .catch(error => console.log(error))



const on = (element, event, selector, handler) => {
    element.addEventListener(event, e =>{
        if(e.target.closest(selector)){
            handler(e)
        }
    })
}



//      Procedimiento Borrar
on(document, 'click', '.btnBorrar', e=>{
    const fila = e.target.parentNode.parentNode
    const id = fila.firstElementChild.innerHTML
    console.log(id)

    alertify.confirm("¿Seguro que desea borrar movimiento?",
    function(){
        setTimeout(() => {alertify.success('Ok')}, 2000);
        
        fetch(url+id,{
            method: 'DELETE'
        })
        .then(() => location.reload())
    },
    function(){
        alertify.error('Cancel')
    })


})



//      Procedimiento Editar
let idForm = 0
on(document, 'click', '.btnEditar', e=>{
    const fila = e.target.parentNode.parentNode
    const idForm = fila.children[0].innerHTML

    const numEmpleadoForm  = fila.children[1].innerHTML
    const mesForm          = fila.children[3].innerHTML
    const cantEntForm      = fila.children[4].innerHTML

    lstEmp()

    id.value          = idForm
    numEmpleado.value = numEmpleadoForm
    mes.value         = definirMesStr(mesForm)
    cantEnt.value     = cantEntForm
    opcion            = 'editar'

    modalMovimientos.show()

})



//      Procedimiento Guardar (en Crear y Editar)
formMovimientos.addEventListener('submit', (e)=>{

    e.preventDefault()
    idForm = id.value
    if(opcion=="crear"){
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type':'application/json'
            },
            body: JSON.stringify({
                numEmpleado : numEmpleado.value,
                mes    : mes.value,
                cantidadEntregas : cantEnt.value
            })
        })
        .then(() => location.reload())

    }


    console.log(opcion)
    if(opcion=="editar"){
        fetch(url+idForm, {
            method: 'PUT',
            headers: {
                'Content-Type':'application/json'
            },
            body: JSON.stringify({
                id     : id.value,
                numEmpleado : numEmpleado.value,
                mes    : mes.value,
                cantidadEntregas : cantEnt.value
            })
        })
        .then(() => location.reload())
            // .then(res => res.json())
            // .then(data =>{
            //     const nuevoEmp = []
            //     nuevoEmp.push(data)
            //     mostrar(nuevoEmp)
            // })
    }
    modalMovimientos.hide()
})
