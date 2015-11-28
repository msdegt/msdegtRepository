		var checkTest = function (){
		var s = $("input");
		var sum = 0;
		var sumPercent = 0;
		var t1 = 0;
		var t2 = 0;
		
	s.each(function(i, elem){
		if($("input:radio").eq(i).prop("checked") == true) {
			if(i==0 || i==6 || i==11){
				sum = sum + 1;                 
			}                   
		}

		if ($("input.t4").eq(i).prop("checked") == true){
			if(i==1 || i==3){
				t1 = t1+0.5;
			}else {
				t1 = t1-0.5;
			}
		}
			
		if ($("input.t5").eq(i).prop("checked") == true){
			if(i==0 || i==1){
				t2 = t2+0.5;
			}else {
				t2 = t2-0.5;
			}
		}		
		
	})
	
	sum = sum + Math.max(t1,0) + Math.max(t2,0);
	
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
	
	$("#result").click(checkTest);