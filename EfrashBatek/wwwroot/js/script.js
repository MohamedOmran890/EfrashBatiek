const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');

allSideMenu.forEach(item => {
	const li = item.parentElement;

	item.addEventListener('click', function () {
		allSideMenu.forEach(i => {
			i.parentElement.classList.remove('active');
		})
		li.classList.add('active');
	})
});




// TOGGLE SIDEBAR
const menuBar = document.querySelector('#content nav .bx.bx-menu');
const sidebar = document.getElementById('sidebar');
const address = document.getElementById("address");
const deaitlsData = document.querySelector(".details");
const form = document.querySelector(".form");
menuBar.addEventListener('click', function () {
	sidebar.classList.toggle('hide');
	sidebar.classList.toggle('d-block');

	if (document.body.contains(form)) {
		form.classList.toggle("ml-5");

	}


	if (document.body.contains(address)) {
		address.classList.toggle("ml-12");
	} else {

		deaitlsData.classList.toggle("ml-5");
	}

})
// End Code Js





const searchButton = document.querySelector('#content nav form .form-input button');
const searchButtonIcon = document.querySelector('#content nav form .form-input button .bx');
const searchForm = document.querySelector('#content nav form');

searchButton.addEventListener('click', function (e) {
	if (window.innerWidth < 576) {
		e.preventDefault();
		searchForm.classList.toggle('show');
		if (searchForm.classList.contains('show')) {
			searchButtonIcon.classList.replace('bx-search', 'bx-x');
		} else {
			searchButtonIcon.classList.replace('bx-x', 'bx-search');
		}
	}
})





if (window.innerWidth < 768) {
	sidebar.classList.add('hide');
} else if (window.innerWidth > 576) {
	searchButtonIcon.classList.replace('bx-x', 'bx-search');
	searchForm.classList.remove('show');
}


window.addEventListener('resize', function () {
	if (this.innerWidth > 576) {
		searchButtonIcon.classList.replace('bx-x', 'bx-search');
		searchForm.classList.remove('show');
	}
})



// const switchMode = document.getElementById('switch-mode');

// switchMode.addEventListener('change', function () {
// 	if (this.checked) {
// 		document.body.classList.add('dark');
// 	} else {
// 		document.body.classList.remove('dark');
// 	}
// })


const deleteItem = (id) => {
	console.log(id);
	const confirmationDialog = document.getElementById("confirmation-dialog");
	const confirmYesButton = document.getElementById("confirm-yes");
	const confirmNoButton = document.getElementById("confirm-no");


	// Show confirmation dialog
	confirmationDialog.style.display = "flex";

	document.getElementById('deletedItemId').innerText = id

	// Listen for "Yes" button click
	confirmYesButton.addEventListener("click", function () {
		const data = {
			id: id
		}

		// Fetch delete item and remove from HTML on success
		/*
		 fetch(url, param)
		 .then(
			const itemToDelete = document.getElementById(id);
			itemToDelete.remove();
			alert("Item deleted successfully");

			// Hide confirmation dialog
			confirmationDialog.style.display = "none";
		 )
		*/

		// Perform deletion
		const itemToDelete = document.getElementById(id);
		itemToDelete.remove();
		alert("Item deleted successfully");

		// Hide confirmation dialog
		confirmationDialog.style.display = "none";
	});

	// Listen for "No" button click
	confirmNoButton.addEventListener("click", function () {
		// Hide confirmation dialog
		confirmationDialog.style.display = "none";
	});
}