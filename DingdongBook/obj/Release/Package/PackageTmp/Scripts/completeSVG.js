function Moveit(el, options) {
    this.el = el;
    this.length = this.getLength();
    this.setupPath(options);
}
Moveit.prototype.getLength = function() {
    if (this.el.nodeName) {
        var tagName = this.el.nodeName.toLowerCase(),
            d;
        if (tagName === 'path') {
            d = this.el.getTotalLength();
        }else if(tagName === 'polygon') {
          var points = this.el.getAttribute('points');
                points = points.split(" ");
            var x1 = null, x2, y1 = null, y2 , d = 0, x3, y3;
            for(var i = 0; i < points.length; i++){
                var coords = points[i].split(",");
                if(x1 == null && y1 == null){

                    if(/(\r\n|\n|\r)/gm.test(coords[0])){
                        coords[0] = coords[0].replace(/(\r\n|\n|\r)/gm,"");
                        coords[0] = coords[0].replace(/\s+/g,"");
                    }

                    if(/(\r\n|\n|\r)/gm.test(coords[1])){
                        coords[0] = coords[1].replace(/(\r\n|\n|\r)/gm,"");
                        coords[0] = coords[1].replace(/\s+/g,"");
                    }

                    x1 = coords[0];
                    y1 = coords[1];
                    x3 = coords[0];
                    y3 = coords[1];

                }else{

                    if(coords[0] != "" && coords[1] != ""){             

                        if(/(\r\n|\n|\r)/gm.test(coords[0])){
                            coords[0] = coords[0].replace(/(\r\n|\n|\r)/gm,"");
                            coords[0] = coords[0].replace(/\s+/g,"");
                        }

                        if(/(\r\n|\n|\r)/gm.test(coords[1])){
                            coords[0] = coords[1].replace(/(\r\n|\n|\r)/gm,"");
                            coords[0] = coords[1].replace(/\s+/g,"");
                        }

                        x2 = coords[0];
                        y2 = coords[1];

                        d += Math.sqrt(Math.pow((x2-x1), 2)+Math.pow((y2-y1),2));

                        x1 = x2;
                        y1 = y2;
                        if(i == points.length-2){
                            d += Math.sqrt(Math.pow((x3-x1), 2)+Math.pow((y3-y1),2));
                        }
                    }
                }
            }
                
        } else if (tagName === 'circle') {
            d = 2 * Math.PI * parseFloat(this.el.getAttribute('r'));
        } else if (tagName === 'rect') {
            d = 2 * this.el.getAttribute('width') + 2 * this.el.getAttribute('height');
        } else if (tagName === 'line') {
            var x1 = this.el.getAttribute('x1');
            var x2 = this.el.getAttribute('x2');
            var y1 = this.el.getAttribute('y1');
            var y2 = this.el.getAttribute('y2');
            d = Math.sqrt(Math.pow((x2 - x1), 2) + Math.pow((y2 - y1), 2));
        }
        return d;
    }
}
Moveit.prototype.clear = function() {
    this.el.style.transition = this.el.style.WebkitTransition = 'none';
}
Moveit.prototype.set = function(options) {
    this.clear();
    if (options.duration === 'undefined') {
        this.setupPath(options);
    } else {
        var timing = (options.timing) ? options.timing : 'linear';
        this.el.style.transition = this.el.style.WebkitTransition =
            'stroke-dashoffset ' + options.duration + 's ' + timing +
            ', stroke-dasharray ' + options.duration + 's ' + timing;
        var delay = (options.delay) ? options.delay : 0;  
        var context = this;
            this.el.addEventListener('transitionend', function(e) {
                if (options.callback || options.follow) { 
                if (e.propertyName === 'stroke-dashoffset' || e.propertyName === 'stroke-dasharray') {
                  if(options.follow) {
                    context.el.style.transition = context.el.style.WebkitTransition = 'none';
                    context.setupPath({
                      start: -(100 - context.getValue(options.start)),
                      end: (context.getValue(options.end) - 100)
                    });
                    delete options.follow;
                  }
                  if(options.callback && typeof options.callback === 'function') {
                    setTimeout(function() {
          if(typeof options.callback === 'function') {
            options.callback();
                      delete options.callback;
          }
                      
                    });
                  }
                  if(options.callback) {
                    //options.callback();
                  }
                    
                }
              }
            }); 
        setTimeout(function() {
            context.setupPath(options);
        }, delay * 1000);
    }
}

Moveit.prototype.getValue = function(val) {
    if(val.toString().indexOf('%') === -1) {
      return val;
    }
    return parseFloat(val.substring(0, val.indexOf('%')));
}
Moveit.prototype.getPercentage = function(val) {
    return (val / 100) * this.length;
}
Moveit.prototype.setupPath = function(options) {
    var start = this.getValue(options.start);
    var end = this.getValue(options.end);
    var dash = (end - start);
    var gap = (100 - (end - start));
    var offset = (100 - start);
    if (options.color) {
        this.el.style.stroke = options.color;
    }
    this.el.style.strokeDasharray = this.getPercentage(dash) +
        ' ' + this.getPercentage(gap);
    this.el.style.strokeDashoffset = this.getPercentage(offset);
}

function outerAnimation() {
    outerCircle.set({
        start: '15%',
        end: '25%',
        duration: 0.2,
        callback: function() {
            outerCircle.set({
                start: '75%',
                end: '150%',
                duration: 0.3,
                follow: true,
                callback: function() {
                    outerCircle.set({
                        start: '70%',
                        end: '75%',
                        duration: 0.3,
                        callback: function() {
                            outerCircle.set({
                                start: '100%',
                                end: '100.1%',
                                duration: 0.4,
                                follow: true,
                                callback: function() {
                                    outerAnimation();
                                    innerAnimation();
                                }
                            })
                        }
                    });
                }
            });
        }
    });
}

function innerAnimation() {
    innerCircle.set({
        start: '20%',
        end: '80%',
        duration: 0.6,
        callback: function() {
            innerCircle.set({
                start: '100%',
                end: '100.1%',
                duration: 0.6,
                follow: true
            });
        }
    });
}

/* Main Script */
var outer = document.querySelector('.outer-path'),
    inner = document.querySelector('.inner-path');
    
var outerCircle = new Moveit(outer, {
    start: '0%',
    end: '0.1%'
});
var innerCircle = new Moveit(inner, {
    start: '0%',
    end: '0.1%'
});
setTimeout(function() {
    outerAnimation();
    innerAnimation();
});