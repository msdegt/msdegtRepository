var p = 0; //.. проверяет первое или второе число 
var memory = 0;
var f = 0; //..проверяет нажата ли функциональная клавиша
var calcTablo = document.getElementById("enter");

	function clicButton(digit){ // функция для ввода чисел
		//calcTablo = document.getElementById("enter");
		if (p == 0) {
			if(calcTablo.value == "0"){
				calcTablo.value = digit;
			}else{
				calcTablo.value = calcTablo.value + digit;
			}
		}else {
			calcTablo.value = "";
			calcTablo.value = digit;
			p = 0;
		}
	}
	
	function operations(fNum){ // функция для функциональных клавиш (для клавиш с математическими операциями)
		if (f > 0){
			rezalt();
		}
		f = fNum;
		p = 1;
		memory = parseFloat(calcTablo.value);
	}
	
	function rezalt() {	// функция для вычисления результата
		p = 1;
		if (f == 1){
			calcTablo.value = memory + parseFloat(calcTablo.value);
		}else if (f == 2) {
			calcTablo.value = memory - parseFloat(calcTablo.value);
		}else if (f == 3) {
			calcTablo.value = memory * parseFloat(calcTablo.value);
		}else if (f == 4){ 
			if (parseFloat(calcTablo.value) == 0){
				alert("На ноль делить нельзя!!!");
			}else {
				calcTablo.value = memory / parseFloat(calcTablo.value);
			}
		}
		f = 0;
	}
	
	function pointDec(){ // функция для десятичной точки
	    if(p!== 0){   
            calcTablo.value = "0.";
			p = 0;
	    }else {
			if (calcTablo.value.indexOf(".")==-1){
				calcTablo.value = calcTablo.value + ".";
			}
		}
    }        
	
	function res(){ // функция сброса
		calcTablo.value = "0";
		p = 0;  
		memory = 0;
		f = 0;
	}
	
	function invers() { // функция инверсии знака
		calcTablo.value = parseFloat(calcTablo.value) * -1;
	}
	
	// назначение обработчиков событий для кнопок
	
document.getElementById("point").onclick = pointDec;
document.getElementById("equal").onclick = rezalt;
document.getElementById("inversion").onclick = invers;
document.getElementById("reset").onclick = res;

var number = document.getElementsByName("numberButton");
for (var i=0; i < number.length; i++){
	number[i].onclick = function(){
		clicButton(this.value);
	}
}

var numberFunc = document.getElementsByName("functionalButton");
for (var i=0; i < numberFunc.length; i++){
	numberFunc[i].onclick = function(){
		operations(Number(this.id)); 
	}
}