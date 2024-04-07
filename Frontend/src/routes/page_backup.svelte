<script lang='ts'>
	import { onMount } from 'svelte';
	import { apiData, categories } from './category.js';

	let doc;

	onMount(() => {
		GetCategories();

		doc = document;
	});

	async function GetCategories() {
		const res = await fetch('http://localhost:5000/Category')
			.then(response => response.json())
			.then(data => {
				console.log(data);
				apiData.set(data);
			}).catch(error => {
				console.log(error);
				return [];
			});
	}

	async function AddCategory(event, element: HTMLElement) {
		// Check if the key pressed is Enter without 'event'
		const categoryName = element.nodeValue;

		const res = await fetch(`http://localhost:5000/Category?name=${categoryName}`, {
			method: 'PUT'
		})
			.then(response => {
				console.log(response.status);

				if (response.status === 200) {
					GetCategories();
				} else {
					CreatePopupSomethingWentWrong(response.status);
				}
			})
			.catch(error => {
				console.log(error);
				return 'something went wrong!';
			});
	}

	async function RemoveCategory(element: HTMLElement) {
		const categoryId = element.nodeValue;

		const res = await fetch(`http://localhost:5000/Category?id=${categoryId}`, {
			method: 'DELETE'
		})
			.then(response => {
				console.log(response.status);

				if (response.status === 200) {
					GetCategories();
				} else {
					CreatePopupSomethingWentWrong(response.status);
				}
			})
			.catch(error => {
				console.log(error);
				return 'something went wrong!';
			});
	}

	// async function UpdateCategory(event, element) {
	// 	console.log('updateCategory was called');
	//
	// 	if (doc) {
	// 		console.log('doc exists');
	// 	} else {
	// 		console.log('doc does not exist');
	// 		return;
	// 	}
	//
	// 	const categoryId = element.value;
	// 	const categoryName = element.value;
	//
	// 	console.log(element.children);
	//
	// 	const res = await fetch(`http://localhost:5000/Category`, {
	// 		method: 'POST',
	// 		body: JSON.stringify({ id: categoryId, name: categoryName })
	// 	})
	// 		.then(response => {
	// 			console.log(response.status);
	//
	// 			if (response.status === 200) {
	// 				GetCategories();
	// 			} else {
	// 				CreatePopupSomethingWentWrong(response.status);
	// 			}
	// 		})
	// 		.catch(error => {
	// 			console.log(error);
	// 			return 'something went wrong!';
	// 		});
	// }

	function CreatePopupSomethingWentWrong(statusCode) {
		alert(`Your request returned ${statusCode}!`);
	}
</script>

<h1>Categories</h1>
<p>
	<label>Get all categories:</label>
	<button on:click={GetCategories}>Fetch</button>
</p>
<!--<p>-->
<!--	<label>Add category from name:</label>-->
<!--	<input on:keyup|preventDefault={e => e.key === 'Enter' && AddCategory(e, e.target)} placeholder="Category name"-->
<!--				 type="text">-->
<!--</p>-->
<p>
	<label>Remove category with Id:</label>
	<input onkeydown="RemoveCategory"
				 pattern="[0-9]"
				 type="text">
</p>

<!--<form onsubmit="UpdateCategory">-->
<!--	<label>Id:</label>-->
<!--	<input id="category_id" placeholder="Category id" type="text">-->
<!--	<br />-->
<!--	<label>Name:</label>-->
<!--	<input id="category_name" placeholder="Category name" type="text">-->
<!--	<br />-->
<!--	<button type="submit">Update</button>-->
<!--</form>-->

<table>
	<thead>
	<tr>
		<th>Id</th>
		<th>Name</th>
	</tr>
	</thead>
	<tbody>
	{#each $categories as [id, name]}
		<tr>
			<td>
				{id}
			</td>
			<td>
				{name}
			</td>
		</tr>
	{/each}
	</tbody>
</table>

<style>
    table, th, td {
        border: 1px solid black;
        border-radius: 20px;
        text-align: center;
    }

    th, td {
        padding: 10px;
    }

    th {
        background-color: green;
    }
</style>
