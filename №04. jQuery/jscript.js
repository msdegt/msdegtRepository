var p = 0; //.. проверяет первое или второе число 
var memory = 0;
var f = 0; //..проверяет нажата ли функциональная клавиша
var calcTablo = $("#enter");

	function clicButton(digit){ // функция для ввода чисел
		if (p == 0) {
			if(calcTablo.val() == "0"){
				calcTablo.val(digit);
			}else{
				calcTablo.val(calcTablo.val()+digit);
			}
		}else {
			calcTablo.val("");
			calcTablo.val(digit);
			p = 0;
		}
	}
	
	function operations(fNum){ // функция для функциональных клавиш (для клавиш с математическими операциями)
		if (f > 0){
			rezalt();
		}
		f = fNum;
		p = 1;
		memory = parseFloat(calcTablo.val());
	}
	
	var rezalt = function() {	// функция для вычисления результата
		p = 1;
		if (f == 1){
			calcTablo.val(memory + parseFloat(calcTablo.val()));
		}else if (f == 2) {
			calcTablo.val(memory - parseFloat(calcTablo.val()));
		}else if (f == 3) {
			calcTablo.val(memory * parseFloat(calcTablo.val()));
		}else if (f == 4){ 
			if (parseFloat(calcTablo.val()) == 0){
				alert("На ноль делить нельзя!!!");
			}else {
				calcTablo.val(memory / parseFloat(calcTablo.val()));
			}
		}
		f = 0;
	}
	
	var pointDec = function(){ // функция для десятичной точки
	    if(p!== 0){   
            calcTablo.val("0.");
			p = 0;
	    }else {
			if (calcTablo.val().indexOf(".") == -1){ ////!!!!
				calcTablo.val(calcTablo.val() + ".");
			}
		}
    }        
	
	var res = function(){ // функция сброса
		calcTablo.val("0");
		p = 0;  
		memory = 0;
		f = 0;
	}
	
	var invers = function() { // функция инверсии знака
		calcTablo.val(parseFloat(calcTablo.val()) * -1);
	}
	
	// назначение обработчиков событий для кнопок

$("#point").click(pointDec);
$("#equal").click(rezalt);
$("#inversion").click(invers);
$("#reset").click(res);


$(function() {
	$(".number").click(function() {
		clicButton($(this).val());
	});
});

$(function() {
	$(".functionalButton").click(function() {
		operations(Number($(this).attr("id")));
	});
});

//$("functionalButton").click(C)

