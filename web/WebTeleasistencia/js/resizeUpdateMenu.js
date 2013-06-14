//(C) Nextgal Soluciones Informáticas, s.l.
//Funciones para cambiar el tamaño al mapa.

function resizeUpdateMenu() {
    //Calculamos el ancho y largo de la ventana del explorador web    
    var scnWid, scnHei;
    if (self.innerHeight) // all except Explorer
    {
        scnWid = self.innerWidth;
        scnHei = self.innerHeight;
    }
    else if (document.documentElement && document.documentElement.clientHeight)
    // Explorer 6 Strict Mode
    {
        scnWid = document.documentElement.clientWidth;
        scnHei = document.documentElement.clientHeight;
    }
    else if (document.body) // other Explorers
    {
        scnWid = document.body.clientWidth;
        scnHei = document.body.clientHeight;
    }

    //Calculamos el ancho del div superior
    var menuHeight = 35;
    var titleHeight = 0;
    var spaceElement = 145;

    //cambiamos el tamaño del mapa para ocupar lo que queda libre de la ventana
    //var map = document.getElementById('ctl00_MainContent_GMap1');
    var map = document.getElementById('map');
    //var htmlheight = document.body.clientHeight;  
    var htmlheight = scnHei - (menuHeight + titleHeight + spaceElement);
    map.style.height = htmlheight + "px";
}