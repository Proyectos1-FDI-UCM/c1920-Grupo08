# SAFE & SOUND

Juego desarrollado para la asignatura de Proyecto 1 del Grado de Desarrollo de Videojuegos de la Universidad Complutense de Madrid, curso 2019/20, Grupo 8.

## OBJETIVOS

El objetivo del jugador es tratar de llegar a la meta final sin que la barra de vida se vacíe pasando por múltiples puntos de control. Durante su recorrido, habrá obstáculos que impedirán al jugador llegar con vida.

El jugador deberá esquivar los obstáculos según avanza, ya sea evitándose de forma  normal o bloqueando estos con el escudo que posea. 

No llevar escudo provocará una forma de jugar más arriesgada, ya que supondrá un riesgo ante todos los escombros y ataques del nivel. A su vez, podrá moverse con mayor rapidez debido a que no lleva ningún otro peso encima, por lo que podrá intentar el desafío de pasarse el nivel rápidamente.

## MECÁNICAS

### **Movimiento**

* **Desplazamiento:** El personaje se mueve a pie a lo largo del eje X.  La cámara sigue al personaje a lo largo de este eje permitiéndole no solo avanzar, sino también retroceder en el nivel.
* **Salto:** Si se pulsa la barra espaciadora el personaje recibirá un impulso vertical que le permitirá sortear distintos obstáculos.
* **Subir o bajar:** Si el personaje está solapando unas escaleras, al pulsar la tecla W, ascenderá hasta llegar a una plataforma superior; si está subiendo unas escaleras o quiere bajar por unas, tendrá que pulsar S.
* **Agacharse:** Reduce la velocidad de movimiento y el tamaño en el eje Y manteniendo pulsada la tecla Ctrl. 

### **Escudos**

* **Protección:** El escudo aparece al lado del personaje al mantener pulsado el botón izquierdo del ratón. Estará direccionado hacia el cursor, lo que le permite una protección de 360º grados.
* **Cambio de escudo:** Si el jugador está solapando un posible nuevo escudo y pulsa la tecla E, desaparecerá el escudo actual, siendo sustituido por el nuevo.
* **Resistencia:** Se muestra en una barra indicando el estado del escudo, y disminuye cuando un objeto colisiona contra el mismo. Cuando esta vida llega a cero, el escudo se rompe, y el jugador se queda sin escudo hasta que coja otro.
Cada escudo tiene una vida inicial distinta, dependiendo de la dureza del material.
* **Peso:** Es otra característica única de cada escudo. Afecta a la velocidad del jugador, haciéndole ir más lento si el escudo es pesado y más rápido si es ligero. Esta modificación de velocidad se suma a la que aparece al agacharse.

### **Salud** 

Es una barra de la interfaz, análoga a la que muestra la vida del escudo, que indica la salud restante del personaje. La vida disminuye cuando algún objeto colisiona contra el cuerpo del personaje. Si la vida llega a cero, el nivel se reiniciará desde el último punto de guardado.

### **Botiquines**
 
Aparecen repartidos por el mapa, como los diferentes escudos. Si el jugador está solapando un botiquín y pulsa el botón E, recuperará el 50% de la salud máxima (100 puntos de vida)

### **Kit de reparación**
 
Son idénticos a los botiquines, pero sirven para restaurar 20 puntos de la barra de resistencia, independientemente del escudo que tengamos equipado.

### **Puntos de control**
 
Tienen forma de walkie-talkie y permiten que el jugador regrese a esa posición después de morir, cambiando la posición de respawn si el jugador encuentra más puntos de control.

### **Puertas y llaves**
 
Las puertas bloquean el camino al jugador hacia varias secciones del juego, que solo pueden ser desbloqueadas con ayuda de llaves repartidas por el mapa. Para poder recoger una llave, debes solapar esta y pulsar E, y no son acumulables.

## HISTORIA

Después de ser reclutado, el escuadrón donde Ben ha sido designado es trasladado hacia el campo de batalla. Tras combatir durante varios meses, los altos mandos del ejército ordenan la retirada de tropas y el abandono de los soldados más débiles, entre los cuales encontramos a Ben. 

Ben despierta solo, en un túnel situado en el centro del campo de batalla civil. Sin deseos de seguir combatiendo, Ben deberá escapar de la ciudad con ayuda de su propio ingenio, todo con el fin de cumplir su objetivo: volver a casa.

## HERRAMIENTAS

* [Unity](https://unity.com/) - Motor.
* [Visual Studio](https://visualstudio.microsoft.com/) - Código.
* [Pivotal](https://www.pivotaltracker.com/) - Historias de usuario.

## DESARROLLADORES

* Emile de Kadt
* Abel Moro Paje
* Eva Sánchez Muñoz
* Gonzalo Fernández
* Cristian Castillo de León
* Pablo Etayo Rodríguez
* Diego Jiménez Gómez

## [DESCARGA](https://proyectos1-fdi-ucm.github.io/c1920-Grupo08/)