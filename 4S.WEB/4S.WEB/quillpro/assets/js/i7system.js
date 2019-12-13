
function left(b,c){if(c<=0){return""}else{if(c>String(b).length){return b}else{return String(b).substring(0,c)}}}function right(c,d){if(d<=0){return""}else{if(d>String(c).length){return c}else{var b=String(c).length;return String(c).substring(b,b-d)}}}

function replaceAll(str, de, para){
    var pos = str.indexOf(de);
    while (pos > -1){
		str = str.replace(de, para);
		pos = str.indexOf(de);
	}
    return (str);
}


function Carrega_Pagina_Div_old(nomeDiv,URL,Parametros,script){


	$.ajax(
	{url:URL,data:Parametros,error:function(){
	alert("Carrega_Pagina_Div:"+URL);
	},success:function(msg){
	alert(msg);	
	}
	})


   $.get( URL, function( msg ) {
   var scriptx='';	
   var nnn=msg.indexOf("[javascript]"); 
   if(nnn>0){
	   nnn+=12;
   scriptx=scriptx+=right(msg,String(msg).length-(nnn));
   nnn-=12;
   msg=left(msg,nnn);
   }
	document.getElementById(nomeDiv).innerHTML=msg;
	if(scriptx!=''){
       eval(scriptx);
	}
	
	
	
	
  //document.getElementById(nomeDiv).innerHTML=msg; 	




});	
}



function Carrega_Pagina_Div(nomeDiv,URL,Parametros,script){
	
	
	
    Parametros1=Parametros;
   	var anterior = document.getElementById(nomeDiv).innerHTML;
	var obj=document.getElementById(nomeDiv);	
	
	
	var msgCirculo='<div style="position:absolute;top:' + $("#" + nomeDiv).offset().top + 'px; left: ' + $("#" + nomeDiv).offset().left + ' px; width:'+obj.offsetWidth+'px; height:'+
        obj.offsetHeight+'px; background-color:#ffffff;opacity:0.85; -moz-opacity:0.85; filter:alpha(opacity=85);"></div>'+
		'<div style="position:absolute;top:'+((obj.offsetHeight-25)/2)+'px; left:'+((obj.offsetWidth-25)/2)+'px; width:25px; height:25px;width:32px; height:32px;"><i class="icon-spinner icon-spin"></i></div>';
	document.getElementById(nomeDiv).innerHTML=document.getElementById(nomeDiv).innerHTML+msgCirculo;
	var req;
    var hora;
    var horaIni;
    hora  = new Date();
    horaIni = hora.getTime(); 
    //nanobots(1,'');
        jQuery.support.cors = true;
	$.ajax({url:URL,data:Parametros1,
		error:function(){
			document.getElementById(nomeDiv).innerHTML = anterior;
			alert(URL);
			alert(Parametros1);
			
		}
		,success:function(msg){
			 hora    = new Date();
             horaFim = (hora.getTime() - horaIni);
			 if ($('#zeroone').length > 0) { 
				 document.getElementById('zeroone').innerHTML=(horaFim /1000);
		}
   if(nomeDiv!=""){
   var scriptx='';
   var nnn1=msg.indexOf("[javascript start]"); 
   var nnn2=msg.indexOf("[javascript end]"); 

   if(nnn1>0 && nnn2>0){

   var p1=left(msg,nnn1-1);
   var p2=right(msg,String(msg).length-((nnn2+16)));
   var p3=left(msg,nnn2);
   var p4=right(p3,String(p3).length-(nnn1+18));
   scriptx=scriptx+p4;
   }

   var nnn=msg.indexOf("[javascript]"); 
   if(nnn>0){
	   nnn+=12;
   scriptx=scriptx+=right(msg,String(msg).length-(nnn));
   nnn-=12;
   msg=left(msg,nnn);
   }
	document.getElementById(nomeDiv).innerHTML=msg;
	if(scriptx!=''){
       eval(scriptx);
	}
	
	
	}
    if(script!=""){
	  var js1=script;
	  for(a=1;a<=js1.length;a++)
	  {js1=js1.replace("´","'")}
	  eval(js1)}
	  
	  }})
	}

function Executa_JS(URL,Parametros,script){
	$.ajax({type:"POST",url:URL,data:Parametros,error:function(){
	alert("Carrega_Pagina_Div:"+URL)
	},success:function(msg){
		eval(msg);
		if(script!=''){
		   eval(script);
		}
		}})}



function enviaForm(URL,nome_div,parametros,limpar,script){
	var b=parametros;
	var l="N";
	var k="";
	var n=document.getElementById(nome_div);
	var d=n.getElementsByTagName("*");
	var g=0;
	var f="";
	//var o="Salvar";
	
	for(i=0;i<d.length;i++){
		if(String(d[i].id).substring(0,8)=="txtCampo"){
			l="S";
            k=d[i].id;
			f=d[i].value;
			if(d[i].type=="checkbox"){if(d[i].checked==false){f=""}}
	        if(d[i].type=="radio"){
		      k=d[i].name;
			  if(d[i].checked==false){l="N"}}
			  
            if(l=="S"){
				for(a=0;a<f.length;a++){
				f=f.replace(" ","|genoma5_espaco|");
			    f=f.replace("&","|genoma5_ecomercial|");
			    f=f.replace("%","|genoma5_percentual|")}
			b+="&"+k+"="+f;g++}
			if(limpar=="S"){d[i].value=""}
}
}
			b+="&qtde="+g;
			URL=replaceAll(URL, "/", String.fromCharCode(92));
			Executa_JS(URL,b,script);
}

function falar(texto){
var is_chrome = navigator.userAgent.toLowerCase();

if(is_chrome.indexOf("chrome") > -1){
     var u = new SpeechSynthesisUtterance();
     u.text = texto;
     u.lang = 'pt-BR';
     u.rate = 2.0;
     u.onend = function(event) {  }
     speechSynthesis.speak(u);
}     
   }


function enviarDiv(j,m,h,c,script){
	var b=h;
	var l="N";
	var k="";
	var n=document.getElementById(m);
	var d=n.getElementsByTagName("*");
	var g=0;var f="";
	var o="Salvar";
	for(i=0;i<d.length;i++){
		if(String(d[i].id).substring(0,4)=="txt_"){
			l="S";k=d[i].id;
			f=d[i].value;if(d[i].type=="checkbox"){
				if(d[i].checked==false){f=""}}
	if(d[i].type=="radio"){
		k=d[i].name;
		//if(k.indexOf("_")>0){k=k.substring(0,k.indexOf("_"))}
			if(d[i].checked==false){l="N"}}
	        if(l=="S"){for(a=0;a<f.length;a++){
	        	f=f.replace(" ","|genoma5_espaco|");
	        	f=f.replace("&","|genoma5_ecomercial|");
	        	f=f.replace("%","|genoma5_percentual|")}
	            b+="&"+k+"="+f;g++}if(c=="S"){d[i].value=""}}}
	            b+="&qtde="+g;
Executa_JS(j,b,script);
}


		
/*
 * Gritter for jQuery
 * http://www.boedesign.com/
 *
 * Copyright (c) 2012 Jordan Boesch
 * Dual licensed under the MIT and GPL licenses.
 *
 * Date: February 24, 2012
 * Version: 1.7.4
 */


(function($){

    /**
     * Set it up as an object under the jQuery namespace
     */
    $.gritter = {};

    /**
     * Set up global options that the user can over-ride
     */
    $.gritter.options = {
        position: '',
        class_name: '', // could be set to 'gritter-light' to use white notifications
        fade_in_speed: 'medium', // how fast notifications fade in
        fade_out_speed: 1000, // how fast the notices fade out
        time: 6000 // hang on the screen for...
    }

    /**
     * Add a gritter notification to the screen
     * @see Gritter#add();
     */
    $.gritter.add = function(params){

        try {
            return Gritter.add(params || {});
        } catch(e) {

            var err = 'Gritter Error: ' + e;
            (typeof(console) != 'undefined' && console.error) ?
                console.error(err, params) :
                alert(err);

        }

    }

    /**
     * Remove a gritter notification from the screen
     * @see Gritter#removeSpecific();
     */
    $.gritter.remove = function(id, params){
        Gritter.removeSpecific(id, params || {});
    }

    /**
     * Remove all notifications
     * @see Gritter#stop();
     */
    $.gritter.removeAll = function(params){
        Gritter.stop(params || {});
    }

    /**
     * Big fat Gritter object
     * @constructor (not really since its object literal)
     */
    var Gritter = {

        // Public - options to over-ride with $.gritter.options in "add"
        position: '',
        fade_in_speed: '',
        fade_out_speed: '',
        time: '',

        // Private - no touchy the private parts
        _custom_timer: 0,
        _item_count: 0,
        _is_setup: 0,
        _tpl_close: '<div class="gritter-close"></div>',
        _tpl_title: '<span class="gritter-title">[[title]]</span>',
        _tpl_item: '<div id="gritter-item-[[number]]" class="gritter-item-wrapper [[item_class]]" style="display:none"><div class="gritter-item">[[close]][[image]]<div class="[[class_name]]">[[title]]<p>[[text]]</p></div><div style="clear:both"></div></div></div>',
        _tpl_wrap: '<div id="gritter-notice-wrapper"></div>',

        /**
         * Add a gritter notification to the screen
         * @param {Object} params The object that contains all the options for drawing the notification
         * @return {Integer} The specific numeric id to that gritter notification
         */
        add: function(params){
            // Handle straight text
            if(typeof(params) == 'string'){
                params = {text:params};
            }

            // We might have some issues if we don't have a title or text!
            if(!params.text){
                throw 'You must supply "text" parameter.';
            }

            // Check the options and set them once
            if(!this._is_setup){
                this._runSetup();
            }

            // Basics
            var title = params.title,
                text = params.text,
                image = params.image || '',
                sticky = params.sticky || false,
                item_class = params.class_name || $.gritter.options.class_name,
                position = $.gritter.options.position,
                time_alive = params.time || '';

            this._verifyWrapper();

            this._item_count++;
            var number = this._item_count,
                tmp = this._tpl_item;

            // Assign callbacks
            $(['before_open', 'after_open', 'before_close', 'after_close']).each(function(i, val){
                Gritter['_' + val + '_' + number] = ($.isFunction(params[val])) ? params[val] : function(){}
            });

            // Reset
            this._custom_timer = 0;

            // A custom fade time set
            if(time_alive){
                this._custom_timer = time_alive;
            }

            var image_str = (image != '') ? '<img src="' + image + '" class="gritter-image" />' : '',
                class_name = (image != '') ? 'gritter-with-image' : 'gritter-without-image';

            // String replacements on the template
            if(title){
                title = this._str_replace('[[title]]',title,this._tpl_title);
            }else{
                title = '';
            }

            tmp = this._str_replace(
                ['[[title]]', '[[text]]', '[[close]]', '[[image]]', '[[number]]', '[[class_name]]', '[[item_class]]'],
                [title, text, this._tpl_close, image_str, this._item_count, class_name, item_class], tmp
            );

            // If it's false, don't show another gritter message
            if(this['_before_open_' + number]() === false){
                return false;
            }

            $('#gritter-notice-wrapper').addClass(position).append(tmp);

            var item = $('#gritter-item-' + this._item_count);

            item.fadeIn(this.fade_in_speed, function(){
                Gritter['_after_open_' + number]($(this));
            });

            if(!sticky){
                this._setFadeTimer(item, number);
            }

            // Bind the hover/unhover states
            $(item).bind('mouseenter mouseleave', function(event){
                if(event.type == 'mouseenter'){
                    if(!sticky){
                        Gritter._restoreItemIfFading($(this), number);
                    }
                }
                else {
                    if(!sticky){
                        Gritter._setFadeTimer($(this), number);
                    }
                }
                Gritter._hoverState($(this), event.type);
            });

            // Clicking (X) makes the perdy thing close
            $(item).find('.gritter-close').click(function(){
                Gritter.removeSpecific(number, {}, null, true);
            });

            return number;

        },

        /**
         * If we don't have any more gritter notifications, get rid of the wrapper using this check
         * @private
         * @param {Integer} unique_id The ID of the element that was just deleted, use it for a callback
         * @param {Object} e The jQuery element that we're going to perform the remove() action on
         * @param {Boolean} manual_close Did we close the gritter dialog with the (X) button
         */
        _countRemoveWrapper: function(unique_id, e, manual_close){

            // Remove it then run the callback function
            e.remove();
            this['_after_close_' + unique_id](e, manual_close);

            // Check if the wrapper is empty, if it is.. remove the wrapper
            if($('.gritter-item-wrapper').length == 0){
                $('#gritter-notice-wrapper').remove();
            }

        },

        /**
         * Fade out an element after it's been on the screen for x amount of time
         * @private
         * @param {Object} e The jQuery element to get rid of
         * @param {Integer} unique_id The id of the element to remove
         * @param {Object} params An optional list of params to set fade speeds etc.
         * @param {Boolean} unbind_events Unbind the mouseenter/mouseleave events if they click (X)
         */
        _fade: function(e, unique_id, params, unbind_events){

            var params = params || {},
                fade = (typeof(params.fade) != 'undefined') ? params.fade : true,
                fade_out_speed = params.speed || this.fade_out_speed,
                manual_close = unbind_events;

            this['_before_close_' + unique_id](e, manual_close);

            // If this is true, then we are coming from clicking the (X)
            if(unbind_events){
                e.unbind('mouseenter mouseleave');
            }

            // Fade it out or remove it
            if(fade){

                e.animate({
                    opacity: 0
                }, fade_out_speed, function(){
                    e.animate({ height: 0 }, 300, function(){
                        Gritter._countRemoveWrapper(unique_id, e, manual_close);
                    })
                })

            }
            else {

                this._countRemoveWrapper(unique_id, e);

            }

        },

        /**
         * Perform actions based on the type of bind (mouseenter, mouseleave)
         * @private
         * @param {Object} e The jQuery element
         * @param {String} type The type of action we're performing: mouseenter or mouseleave
         */
        _hoverState: function(e, type){

            // Change the border styles and add the (X) close button when you hover
            if(type == 'mouseenter'){

                e.addClass('hover');

                // Show close button
                e.find('.gritter-close').show();

            }
            // Remove the border styles and hide (X) close button when you mouse out
            else {

                e.removeClass('hover');

                // Hide close button
                e.find('.gritter-close').hide();

            }

        },

        /**
         * Remove a specific notification based on an ID
         * @param {Integer} unique_id The ID used to delete a specific notification
         * @param {Object} params A set of options passed in to determine how to get rid of it
         * @param {Object} e The jQuery element that we're "fading" then removing
         * @param {Boolean} unbind_events If we clicked on the (X) we set this to true to unbind mouseenter/mouseleave
         */
        removeSpecific: function(unique_id, params, e, unbind_events){

            if(!e){
                var e = $('#gritter-item-' + unique_id);
            }

            // We set the fourth param to let the _fade function know to
            // unbind the "mouseleave" event.  Once you click (X) there's no going back!
            this._fade(e, unique_id, params || {}, unbind_events);

        },

        /**
         * If the item is fading out and we hover over it, restore it!
         * @private
         * @param {Object} e The HTML element to remove
         * @param {Integer} unique_id The ID of the element
         */
        _restoreItemIfFading: function(e, unique_id){

            clearTimeout(this['_int_id_' + unique_id]);
            e.stop().css({ opacity: '', height: '' });

        },

        /**
         * Setup the global options - only once
         * @private
         */
        _runSetup: function(){

            for(opt in $.gritter.options){
                this[opt] = $.gritter.options[opt];
            }
            this._is_setup = 1;

        },

        /**
         * Set the notification to fade out after a certain amount of time
         * @private
         * @param {Object} item The HTML element we're dealing with
         * @param {Integer} unique_id The ID of the element
         */
        _setFadeTimer: function(e, unique_id){

            var timer_str = (this._custom_timer) ? this._custom_timer : this.time;
            this['_int_id_' + unique_id] = setTimeout(function(){
                Gritter._fade(e, unique_id);
            }, timer_str);

        },

        /**
         * Bring everything to a halt
         * @param {Object} params A list of callback functions to pass when all notifications are removed
         */
        stop: function(params){

            // callbacks (if passed)
            var before_close = ($.isFunction(params.before_close)) ? params.before_close : function(){};
            var after_close = ($.isFunction(params.after_close)) ? params.after_close : function(){};

            var wrap = $('#gritter-notice-wrapper');
            before_close(wrap);
            wrap.fadeOut(function(){
                $(this).remove();
                after_close();
            });

        },

        /**
         * An extremely handy PHP function ported to JS, works well for templating
         * @private
         * @param {String/Array} search A list of things to search for
         * @param {String/Array} replace A list of things to replace the searches with
         * @return {String} sa The output
         */
        _str_replace: function(search, replace, subject, count){

            var i = 0, j = 0, temp = '', repl = '', sl = 0, fl = 0,
                f = [].concat(search),
                r = [].concat(replace),
                s = subject,
                ra = r instanceof Array, sa = s instanceof Array;
            s = [].concat(s);

            if(count){
                this.window[count] = 0;
            }

            for(i = 0, sl = s.length; i < sl; i++){

                if(s[i] === ''){
                    continue;
                }

                for (j = 0, fl = f.length; j < fl; j++){

                    temp = s[i] + '';
                    repl = ra ? (r[j] !== undefined ? r[j] : '') : r[0];
                    s[i] = (temp).split(f[j]).join(repl);

                    if(count && s[i] !== temp){
                        this.window[count] += (temp.length-s[i].length) / f[j].length;
                    }

                }
            }

            return sa ? s : s[0];

        },

        /**
         * A check to make sure we have something to wrap our notices with
         * @private
         */
        _verifyWrapper: function(){

            if($('#gritter-notice-wrapper').length == 0){
                $('body').append(this._tpl_wrap);
            }

        }

    }

})(jQuery);





function alerta(tipo, titulo, conteudo){
	if(tipo == 1){return Growl.success({title: titulo, text:conteudo});}
	if(tipo == 2){return Growl.error({title: titulo, text:conteudo});}
	if(tipo == 3){return Growl.warn({title: titulo, text:conteudo });}
	if(tipo == 4){return Growl.info({title: titulo, text:conteudo });}
}



(function() {

  this.Growl = (function() {

    function Growl() {}

    Growl.info = function(options) {
      options['class_name'] = "info";
      options.title = "<i class='icon-info-sign'></i> " + options.title;
      return $.gritter.add(options);
    };

    Growl.warn = function(options) {
      options['class_name'] = "warn";
      options.title = "<i class='icon-warning-sign'></i> " + options.title;
      return $.gritter.add(options);
    };

    Growl.error = function(options) {
      options['class_name'] = "error";
      options.title = "<i class='icon-exclamation-sign'></i> " + options.title;
      return $.gritter.add(options);
    };

    Growl.success = function(options) {
      options['class_name'] = "success";
      options.title = "<i class='icon-ok-sign'></i> " + options.title;
      return $.gritter.add(options);
    };

    Growl.mensagem = function(options) {
      options['class_name'] = "mensagem";
      options.title = "<i class='icon-ok-sign'></i>" + options.title;
      return $.gritter.add(options);
    };

    return Growl;

  })();

}).call(this);

function envia_xml(texto_xml){
	xml_enviar="x=<token>8505308f2217249859800efe9ed11142</token>"+texto_xml;
	$.ajax({
	 type: "GET",
	 data: xml_enviar,
	 url: "http://pof3.web7010.uni5.net/controle.html",
	 contentType: "text/xml",
	 dataType: "xml",
	 cache: false,	 
	 success: function(xml){
		 alert(xml);
	  },
	 error: function() {
		alert("deu pau");
	  }
	  });
		return "ok";
}

//function envia_xml(url){
//var xml = "" + 
//"<?xml version='1.0' encoding='UTF-8'?>" +
//"<methodCall>" +
 // "<methodName>ContactService.add</methodName>" +
 // "<params>" +
 // "  <param>" +
 // "    <value><string>privateKey</string></value>" +
 // "  </param>" +
 // "  <param>" +
 // "    <value><struct>" +
 // "      <member><name>FirstName</name>" +
 // "        <value><string>John</string></value>" +
 // "      </member>" +
 // "      <member><name>LastName</name>" +
 // "        <value><string>Doe</string></value>" +
 // "      </member>" +
 // "      <member><name>Email</name>" +
 // "        <value><string>there_he_go@itsjohndoe.com</string></value>" +
 // "      </member>" +
 // "    </struct></value>" +
 // "  </param>" +
 // "</params>" +
 // "</methodCall>";


//	$.ajax({type:"POST",url:URL,data:Parametros,error:function(){
//	alert("Carrega_Pagina_Div:"+URL)
//	},success:function(msg){
//		eval(msg);
//		}})}

//    URL="http://pof3.web7010.uni5.net/controle.html";
 //   $.ajax({
 //   url: URL,
 //   data: "<test>BOM DIA</test>", 
 //   type: 'POST',
 //   contentType: "text/xml",
 //   dataType: "text",
   // success : parse,
//	success:function(msg){
//		eval(msg);
//	}
  //  error : function (xhr, ajaxOptions, thrownError){  
  //      console.log(xhr.status);          
  //      console.log(thrownError);
  //  } 

  
 // return "";
  
//  }
  
  
  



