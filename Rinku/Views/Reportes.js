// Definición de variables:
const url = "https://localhost:7168/api/Reportes/"
const urlEmpleados = "https://localhost:7168/api/Empleados/"
const urlMovimientos = "https://localhost:7168/api/Movimientos/"

const contenedor           = document.getElementById('tbodyTablaMovs')
const tbodyTabCalculos     = document.getElementById('tbodyTabCalculos')

const filtroSelectEmpleado = document.getElementById('filtroSelectEmpleado')
const filtroMesIni         = document.getElementById('filtroMesIni')
const filtroMesFin         = document.getElementById('filtroMesFin')
let resultados = ''
let listEmpleados = ''


//  Variables para llevar la sumatoria de los reportes
let sumCantEnt      = 0
let sumSueldoBruto  = 0
let sumISR          = 0
let sumISRadicional = 0
let sumVales        = 0
let sumSueldoNeto   = 0
let sumasHTML       = ""



const modalMovimientos = new bootstrap.Modal(document.getElementById('modalMovimientos'))
const formMovimientos = document.getElementById('formMovimientos')

let opcion = ''
let dataEmpResp = []


// Traigo info de los empleados
fetch(urlEmpleados)
    .then(response => response.json())
    .then(data => lstEmp(data))
    .catch(error => console.log(error))

// Función para cargar lista de empleados:
function lstEmp (_data) {
    console.log(_data)
    dataEmpResp = _data


    listEmpleados = ''
    listEmpleados += `
        <option value="0">Todos los empleados</option>
        `
        _data.forEach(emp => {

        listEmpleados += `
        <option value="${emp.id}">${emp.nombre}</option>
        `
    })

    filtroSelectEmpleado.innerHTML = listEmpleados
}


function getNombreEmp(_idEmp) {
    let name = ""
    dataEmpResp.forEach(e => {
        if (_idEmp == e.id) {
            name = e.nombre
        }
    })
    return name
}



// Función para mostrar resultados:
const mostrar = (movimientos) => {
    resultados = ''

    sumCantEnt      = 0
    sumSueldoBruto  = 0
    sumISR          = 0
    sumISRadicional = 0
    sumVales        = 0
    sumSueldoNeto   = 0


    movimientos.forEach(mov => {
        resultados += `
            <tr>
                <td>${mov.id}</td>
                <td>${mov.numEmpleado}</td>
                <td>${getNombreEmp(mov.numEmpleado)}</td>
                <td>${definirMes(mov.mes)}</td>
                <td>${mov.cantidadEntregas}</td>
                <td>${mov.sueldoBruto}</td>
                <td> -${mov.isr}</td>
                <td> -${mov.isrAdicional}</td>
                <td>${mov.vales}</td>
                <td>${mov.sueldoNeto}</td>
                <td class="text-center"> <a class="btnEditar btn btn-primary">Ver Empleado</a> </td>
            </tr>
            `

        sumCantEnt      += mov.cantidadEntregas
        sumSueldoBruto  += mov.sueldoBruto
        sumISR          += mov.isr
        sumISRadicional += mov.isrAdicional
        sumVales        += mov.vales
        sumSueldoNeto   += mov.sueldoNeto
                 
    });

    contenedor.innerHTML = resultados

    sumasHTML = ""
    sumasHTML = `
        <tr>
            <td>${sumCantEnt}</td>
            <td>${sumSueldoBruto}</td>
            <td> -${sumISR}</td>
            <td> -${sumISRadicional}</td>
            <td>${sumVales}</td>
            <td>${sumSueldoNeto}</td>
        </tr>
    `
    tbodyTabCalculos.innerHTML = sumasHTML

}

const definirMes = (numMes) => {
    switch (numMes) {
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

const definirMesStr = (StrMes) => {
    switch (StrMes) {
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
    element.addEventListener(event, e => {
        if (e.target.closest(selector)) {
            handler(e)
        }
    })
}


//      Procedimiento Guardar (en Crear y Editar)
formFiltros.addEventListener('submit', (e) => {

    e.preventDefault()
    
    let strUrl = ""
    if(filtroMesIni.value!=0 || filtroMesFin.value!=0 || filtroSelectEmpleado.value!=0 )
    {
        strUrl     = `?mesIni=${filtroMesIni.value}&mesFin=${filtroMesFin.value}&idEmp=${filtroSelectEmpleado.value}`
    }


    fetch(url+strUrl)
        .then(response => response.json())
        .then(data => mostrar(data))
        .catch(error => console.log(error))

})
