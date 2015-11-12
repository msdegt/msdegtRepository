		var mass = [];
		var k = 0;
		var b = 0;
		
		for (; ; ){
			k = prompt("Введите любое число",'');
			if (k == "" || isNaN(k) || k == null) {break;} //isNaN(k),т.к.значение NaN не может быть проверено операторами эквивалентности.!!!
			else {mass.push(Number(k));} // преобразование в числo...или можно слово Number заменить на +, т.е. +k!!!
		}
		for (var i = 0; i < mass.length; i++) {
			b = b + mass[i];
		}
		alert(b);