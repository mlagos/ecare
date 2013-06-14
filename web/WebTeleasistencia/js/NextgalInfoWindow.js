/** 
  * Check to see if the displayed extInfoWindow is positioned off the viewable  
  * map region and by how much.  Use that information to pan the map so that  
  * the extInfoWindow is completely displayed. 
  */ 
function PanMap(map,padX,padY,marker){ 
   this.map_ = map;
   this.paddingX_ = padX;
   this.paddingY_ = padY;
   this.marker_ = marker;
   this.maxPanning_ = 500;
   
   //pan if necessary so it shows on the screen 
   var mapNE = this.map_.fromLatLngToDivPixel( 
     this.map_.getBounds().getNorthEast() 
   ); 
   var mapSW = this.map_.fromLatLngToDivPixel( 
     this.map_.getBounds().getSouthWest() 
   ); 
   var markerPosition = this.map_.fromLatLngToDivPixel( 
     this.marker_.getPoint() 
   ); 
  
   var panX = 0; 
   var panY = 0; 
   var paddingX = this.paddingX_; 
   var paddingY = this.paddingY_; 
   var infoWindowAnchor = this.marker_.getIcon().infoWindowAnchor; 
   var iconAnchor = this.marker_.getIcon().iconAnchor; 
  
   var offsetTop = markerPosition.y - ( -infoWindowAnchor.y + iconAnchor.y +  400 + this.paddingY_); 
   if (offsetTop < mapNE.y) { 
     panY = mapNE.y - offsetTop; 
   } else { 
     //test bottom of screen (but don't go past top boundary) 
     var offsetBottom = markerPosition.y + this.paddingY_; 
     if (offsetBottom >= mapSW.y) { 
                  panY = Math.max( -(offsetBottom - mapSW.y), mapNE.y - offsetTop); 
     } 
   }
   //test right of screen 
   var offsetRight = markerPosition.x + 500 + this.paddingX_ + infoWindowAnchor.x - iconAnchor.x; 
   if (offsetRight > mapNE.x) { 
     panX = -( offsetRight - mapNE.x);
   } else { 
     //test left of screen 
     var offsetLeft = - (Math.round(( 250 - this.marker_.getIcon().iconSize.width/2) + 250 + this.paddingX_) - markerPosition.x - infoWindowAnchor.x + iconAnchor.x); 
     if( offsetLeft < mapSW.x) { 
       panX = mapSW.x - offsetLeft; 
     } 
   }
   
   if ((panX != 0 || panY != 0 )) { 
       if ((panY < 0 - this.maxPanning_ || panY > this.maxPanning_) && (panX < 0 - this.maxPanning_ || panX > this.maxPanning_)) {  
         this.map_.setCenter(this.marker_.getPoint()); 
       }else { 
         this.map_.panBy(new GSize(panX,panY)); 
       } 
   } 
 };
