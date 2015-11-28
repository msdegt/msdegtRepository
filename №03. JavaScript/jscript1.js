
	function checkTest(){
		var a = document.getElementsByName("t1");
		var b = document.getElementsByName("t2");
		var c = document.getElementsByName("t3");
		var d = document.getElementsByName("t4");
		var e = document.getElementsByName("t5");
		var sum = 0;
		var sumPercent = 0;
		var t = 0;
		
		 if (a[0].checked == true){
			sum = sum + 1;
		}
		 if (b[2].checked == true){
			sum = sum + 1;
		}
		if (c[3].checked == true) {
			 sum = sum + 1;
		}
		
		///////////////////////
		
		if (d[0].checked == true) {
			t = t - 0.5;
		}
		if (d[1].checked == true){
			t = t + 0.5;
		}
		if (d[2].checked == true){
			t = t - 0.5;
		}
		if (d[3].checked == true){
			t = t + 0.5;
		}
		sum = sum + Math.max(t,0);
		
		//////////////////////
		t=0;
		 
		if (e[0].checked == true){
			t = t + 0.5;
		}
		if (e[1].checked == true){
			t = t + 0.5;
		}
		if (e[2].checked == true){
			t = t - 0.5;
		}
		if (e[3].checked == true){
			t = t - 0.5;
		}
		sum = sum + Math.max(t,0);
		sumPercent = (sum / 5) * 100;
		
		switch (true){
			case sumPercent < 25:
				alert("Совсем печально! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent < 50:
				alert("Плохо! Учите дальше. Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent < 75:
				alert("Могло быть лучше! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent < 100:
				alert("Хорошо! Есть к чему стремиться! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			case sumPercent == 100:
				alert("Отлично! Продолжайте в том же духе! Ваш результат: " + sum + " балла(ов), " + "что составило от общего числа правильных ответов: " + sumPercent + "%");
				break;
			 default:
			 alert("Я таких значений не знаю!");
		}
	}
	document.getElementById("result").onclick = checkTest;