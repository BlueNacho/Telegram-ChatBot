# Aclaraciones
-----------------------------------------------
Para comenzar a utilizar el bot debemos escribirle "Hola". De esta manera el bot comienza y nos muestra el 
menú de bienvenida.

Para acceder al administrador tendrá que descomentar unas lineas de código que se encuentran en el archivo de Program (las mismas se encuentran con referencias e indicaciones para entenderlas facilmente)

Si ya se ingresó al sistema como un trabajador o empleador y desea acceder al perfil de administrador, deberá borrar dicho usuario en la persistencia correspondiente (de empleador o trabajador).

En el program se ven cargados unos casos de prueba para los servicios, así el bot se veia más completo cuando los filtraba por ejemplo (es decir, que devolvía algo).

Para el correcto funcionamiento del programa, se debe haber ingresado con nombre de usuario, además de 
tener nombre y apellido. En caso de no tener alguno de ellos,los métodos no funcionaran debido a las 
excepciones que chequean de que hayan sido ingresados.  
Para poseer un nombre de usuario, la persona debe en telegram, apretar en la izquierda arria de la interfaz (hay 3 barritas), luego apretar settings y agregar
un Username. 

# Desafíos que se nos presentaron en la segunda entrega.
 -----------------------------------------------

Como equipo se nos plantearon varios desafíos para esta entrega. A la hora de empezar a programar las 
clases que habiamos diseñado
para la primera entrega, nos encontramos con algunos problemas de lógica que tuvimos que repensar para 
lograr resolverlos, y otras clases tuvieron que ser rediseñadas
para perfeccionarlas con las correcciones realizadas por los docentes. 
Otro gran desafío que se nos planteo fue distribuir la cantidad de tarea para que quede parejo y acorde 
con las virtudes de cada integrante al momento de programar.


# Desafíos que se nos presentaron en la tercera entrega.
 -----------------------------------------------
 
En la tercera entrega el desafío que se nos presento fue el lograr combinar la lógica y los Handlers. Si 
bien al aplicar los patrones que mencionamos más abajo en el READ ME te facilita la integración del bot 
de Telegram, debimos chequear minusociamente los estados del Handler con los diferentes métodos a lo 
largo de la ejecutación de cada acción. 
Otro gran desafío que afrontamos fue el hecho de organizar de manera correcta el tiempo y 
responsabilidades. Una buena organización es crucial para lograr llegar a la fecha límite de entrega con 
un programa correcto. 
Por último, el hecho de ser 3 personas en el equipo nos presentó un desafío porque eran muchas 
responsabilidades distribuidas en un grupo chico, pero como mencionamos previamente, una buena 
organización fue clave del llegar a la fecha límite con una entrega de la cual quedaramos satisfechos de 
lo que habíamos logrado.

# ¿Qué aprendimos por fuera de clase?
 -----------------------------------------------
Por fuera de clase tuvimos que familiarizarnos con el State Pattern y el Chain Of Responsability, 
conceptos claves para el buen funcionamiento del Bot de Telegram. 
Fueron conceptos que fuimos aprendiendo a medida que hacíamos los Handlers y nos topabamos con obstaculos 
de lógica, que estos nos ayudaron a resolver. Por ejemplo, 
como guiar el proceso de como funcionaba el handler, o cuando debíamos implementar la lógica para que el 
programa conozca a que handler le correspondía responder al 
mensaje. 

# Principios
-----------------------------------------------

###### CREATOR

El principio se ve reflejado a la hora de crear nuestras clases. 
En la clase gestión contrato, a la hora de utilizar el método CrearContrato(), adentro del mismo crea un 
objeto de tipo contrato.
Otro ejemplo de este principio reflejado en nuestras clases es en el método OfrecerServicio() del 
trabajado. El trabajador tiene un método por el cual puede ofrecer un servicio, y este se encarga de crear 
un objeto de tipo servicio. 
GestionUsuario presenta un método para crear empleadores llamado CrearEmpleador() que se encarga de crear 
un objeto de tipo Empleador con los datos pasados por parametro. 

###### SRP y EXPERT 

La lógica fue diseñada en base a que las clases no tuvieran más de una responsabilidad. El crear varias 
clases nos permitió delegar responsabilidades y que no queden sobrecargadas de responsabilidades de las 
cuales no deben hacerse responsables. 
Hay varios ejemplos a lo largo de la lógica utilizada, uno es la clase CatalogoCategoría, que se encarga 
de verificar que exista cierta categoría dentro de las existentes, o de lo contrario, tambien se encarga 
de añadir categorías al catálogo. Estas responsabilidades no pueden pertenecer al admin, a pesar de que el 
es el encargado de controlar que categorías se deben añadir. 
Otro ejemplo es el de catologo servicio, el cual se encarga de agregar o borrar servicios de la lista de 
servicios disponibles. La clase servicio no puede tener la responsabilidad de añadirse a si misma a una 
lista de servicios disponibles, asimismo, servicio unicamente presenta un constructor, el cual es 
utilizado en Trabajador para cuando quiere ofrecer un servicio. 
Y el último Catalogo que creamos fue Catálogo Contrato. El cual se encarga de comprobar, y cambiar estados 
de los contratos.
Estos Catalalogos son los encargados de realizar dichas acciones previamente debido a que son los que 
conocen la información correspondiente para llevarlas a cabo. 
Previo a crearlos, habíamos analizado dejarle las responsabilidades previamente mencionadas a las clases 
Servicio, Contrato, Categoría fue cambiando de nombre.
Pero pensandolo bien, resolvimos en conjunto que las responsabilidades de conocer dicho estado no debía 
ser de ellos, no nos hacía sentido el hecho de que un contrato
deba conocer en que estado se encuentra, pero si que deje que su estado se conozca mediante un método de 
otra clase. 

Siguiendo con SRP, tenemos las clases:
*Estado : Encargada de contener estados, que utilizaremos en contratos para definir en que estado se 
encuentra cada uno .

*IDGenerator : Encargada de generar ID´s para los servicios que ofrecen los trabajadores. Para los 
trabajadores no era necesario crear ID´s personales porque 
no las proporciona telegram.

###### ISP

Interface Segregation Principle es un principio que nos permite delimitar que usuarios pueden ser 
calificados. Si no todo iba a poder ser calificable, sin la interfaz 
ICalificable esto no podría haberse solucionado. Esta interfaz contiene una lista de calificaciones, la 
cual utilizamos para calcular la calificación promediada 
gracias a todos los elementos que se encuentran en ella. 


# Patrones de diseño
-----------------------------------------------

###### Singleton

Es un patron de diseño que nos permite crear una única instancia de una clase. En nuestro programa tenemos 
casos donde debemos tener una única lista de una clase 
para toda la lógica y que esta no se vea afectada. Para utilizarlas, utilizamos instancias del singleton 
de la clase que necesitamos. 

###### Chain of responsability 

Para los handlers, tendremos una solicitud de mensaje donde se analizara el mensaje emitido por el 
usuario. Según lo que recibe, el mensaje pasará por "la cadena" 
donde chequeará en "cada eslabon" si corresponde a el o no el dar una respuesta ese mensaje. Por ejemplo, 
en el menú inicial o principal tenemos varias opciones, y, 
para seleccionar una de ellas, el usuario debe ingresar mediante un mensaje la opción que desee ejecutar. 
El programa va a identificar el mensaje que fue emitido, y 
chequeará en los handlers a cual le corresponde ese mensaje. 


# Patrones de conducta
-----------------------------------------------
###### State pattern

El State pattern es un patron de conducta que permite cambiar el comportamiento de un objeto según el 
estado que este posea. 
Este fue utilizado en todos los handlers del Telegram.
Para realizar el proceso guíado de todos los métodos del programa, fuimos utilizando estados que nos 
permitieran chequear en que parte del proceso se encontraban.
Por mencionar un ejemplo, en el handler llamado ServicioHandler cuando se lo llama mediante el comando 
Ofrecer Servicio, el estado del handler se encuentra en Start. Una vez que el usuario indica y sigue las 
instrucciones que el BOT da, el handler pasa al siguiente estado el cual se denomina "Checking". En este 
estado, se chequearan los datos ingresados por el usuario. En caso de que se hayan ingresado mal, se 
reinicia al estado inicial el cual es "Start", permitiendo así volver a ingresarlos de forma correcta. En 
caso de que se chequeen de manera correcta, cambiará el estado a Completed con el servicio ya creado con 
los parámetros ingresados en el bot. 
Como mencione previamente, los estados del Handler son sumamente necesarios para evitar problemas en la 
ejecución del proceso. 

-----------------------------------------------

¿Quién estuvo encargado de cada clase?.

###### Sofía Guerrico
Contrato, CatlogoContrato, GestionUsuario, CrearContratoHandler, GestionContratoHandler, 
MenuPrincipalHandler, BuscarOfertas, VerContratos, VerMiCalificacion, 
AceptarContrato,FinalizarContrato,CrearContrato, VerServicioHandler.

###### Ignacio Berra
Empleador, Trabajador, UsuarioComun, Usuario, Calificacion, Servicio, NotificacionesHandler, 
OfrecerServicioHandler, VerCalificacionHandler, CalificarHandler, Tests, Doxygen, Documentación proyecto, Excepciones.

###### Fernando Sánchez
Administrador.

###### Ignacio Tachini
CatalogoServicio, CatalogoCategoria, FiltroBusqueda, Notificaciones, UtilidadesCalificacion, 
CalculadorDistancia, Estado, IDGenerator, BorrarServicio, CrearCategoriaHandler, Tests, Excepciones.

Vale aclarar que por más que las clases se vean asignadas de tal modo, aun así cada integrante interactuó 
con otras más ya que era necesaria su manipulación para hacer funcionar sus respectivas clases

# Reflexión
 -----------------------------------------------
 El proyecto fue un momento de mucho aprendizaje, donde tuvimos que aplicar conceptos aprendidos a lo largo del semestre. Además de aprender, el tener que aplicar
 lo aprendido en un proyecto de este estilo, implica que te hayas familiarizado con los patrones y diseño por contrato. 
 Quitando la parte de programación, hacer un proyecto de estas dimensiones requiere de desarrollar hablidades comunicacionales para lograr entenderte con tu equipo
 y tomar las mejores decisiones en conjunto permite lograr alcanzar mejores resultados. 

# Bibliografía
 -----------------------------------------------
- Los patrones que mencionamos en las secciones del READ-ME fueron consultados en : https://refactoring.guru/design-patterns . 
- Lecturas de la organización UCUDAL en GitHub: https://github.com/ucudal .

