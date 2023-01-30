// Definición de variables:
const url = "https://localhost:7168/api/Empleados/"
const contenedor = document.querySelector('tbody')
let resultados = ''


const modalEmpleados = new bootstrap.Modal(document.getElementById('modalEmpleados'))
const formEmpleados = document.getElementById('formEmpleados')
const id = document.getElementById('idEmp')
const numero = document.getElementById('numero')
const nombre = document.getElementById('nombre')
const rol = document.getElementById('rol')
let opcion = ''
let maxId = 0


btnCrear.addEventListener('click', ()=>{

    //fetch(url+"max")
    //.then(console.log(response.body))
////    .then(console.log(data))
  //  .catch(error => console.log(error))


    id.value = maxId
    numero.value = ''
    nombre.value = ''
    rol.value = ''
    opcion = 'crear'

    modalEmpleados.show()
})


// Función para mostrar resultados:
const mostrar = (empleados) => {
    empleados.forEach(empleado =>{
        if(empleado.id > maxId){
            maxId = empleado.id
        }

        resultados += `
            <tr>
                <td>${empleado.id}</td>
                <td>${empleado.numero}</td>
                <td>${empleado.nombre}</td>
                <td>${definirRol(empleado.rol)}</td>
                <td class="text-center"> <a class="btnEditar btn btn-primary">Editar</a> <a class="btnBorrar btn btn-danger">Borrar</a> </td>
            </tr>
            `        
    });

    maxId ++
    contenedor.innerHTML = resultados

}


const definirRol = (numRol) =>{
    switch(numRol){
        case 1:
            return "Chofer"

        case 2:
            return "Cargador"
            
        case 3:
            return "Auxiliar"
    }
}

const definirRolStr = (StrRol) =>{
    switch(StrRol){
        case "Chofer":
            return 1

        case "Cargador":
            return 2
            
        case "Auxiliar":
            return 3
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

    alertify.confirm("¿Seguro que desea borrar empleado?",
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

    const numeroForm = fila.children[1].innerHTML
    const nombreForm = fila.children[2].innerHTML
    const rolForm    = fila.children[3].innerHTML


    id.value     = idForm
    numero.value = numeroForm
    nombre.value = nombreForm
    rol.value    = definirRolStr(rolForm)
    opcion       = 'editar'

    modalEmpleados.show()

})



//      Procedimiento Guardar (en Crear y Editar)
formEmpleados.addEventListener('submit', (e)=>{

    e.preventDefault()
    idForm = id.value
    if(opcion=="crear"){
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type':'application/json'
            },
            body: JSON.stringify({
                numero : numero.value,
                nombre : nombre.value,
                rol    : rol.value
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
                numero : numero.value,
                nombre : nombre.value,
                rol    : rol.value,
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
    modalEmpleados.hide()
})

