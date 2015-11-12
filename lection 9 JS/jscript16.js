	var user = {};
		user.name = "Вася";
		user.surname = "Петров";
		alert (user.name + " " + user.surname);
		user.name = "Сергей";
		alert (user.name);
		delete user.name;
		if (user.name===undefined) {
			alert ("Свойство отсутствует");
		}