Api Rest Masiv
Api que implementa las reglas de Clean Code de Masiv, implementadas en esta ruleta de apuestas virtual. Este Api consta de los siguientes EndPoints:
1. Endpoint de creación de nuevas ruletas que devuelva el id de la nueva ruleta creada
2. Endpoint de apertura de ruleta (el input es un id de ruleta) que permita las
	posteriores peticiones de apuestas, este debe devolver simplemente un estado que
	confirme que la operación fue exitosa o denegada.
3. Endpoint de apuesta a un número (los números válidos para apostar son del 0 al 36)
	o color (negro o rojo) de la ruleta una cantidad determinada de dinero (máximo
	10.000 dólares) a una ruleta abierta.
4. Endpoint de cierre apuestas dado un id de ruleta, este endpoint debe devolver el
	resultado de las apuestas hechas desde su apertura hasta el cierre.
	El número ganador se debe seleccionar automáticamente por la aplicación al cerrar
	la ruleta y para las apuestas de tipo numérico se debe entregar 5 veces el dinero
	apostado si atinan al número ganador, para las apuestas de color se debe entrega 1.8
	veces el dinero apostado, todos los demás perderán el dinero apostado.
5. Endpoint de listado de ruletas creadas con sus estados (abierta o cerrada)
Pre-requisitos 📋
Para realizar las pruebas de los EndPoints, es necesario contar con la herramienta Postman y visual studio 2019

Ejecutando las pruebas ⚙️
* Nueva Ruleta: Petición Post
https://localhost:44322/api/Roulette/NewRoulette
Json:
{
    "id": 1,
    "isOpenRulette": false
}


* Por defecto todas las ruletas se crearan con un estado cerrado

* Abriendo o habilitando una ruleta: Peticion Put

https://localhost:44322/api/Roulette/OpenRulette/{Id_Ruleta} 


* Registrando una apuesta: Peticion Post

Para esta peticion es necesario agregar un Headers, el cual sera :  Id-User 

https://localhost:44322/api/Roulette/RouletteBet/{ID_Ruleta} 



En el Body especificamos el numero al que apostaremos y el monto de la apuesta.

{
    "number": 22,
    "money": 6000
}


*Cierre de Ruleta y Resultados: Petición Put

https://localhost:44322/api/Roulette/CloseRulette/{Id_Ruleta}

Este método cerrara la ruleta y mostrara quines ganaron o perdieron según las apuestas asignadas a esta.


*Consulta de Ruletas: Petición GET


Esta petición retorna las ruletas creadas y sus estados, es el método principal y por defecto del api.