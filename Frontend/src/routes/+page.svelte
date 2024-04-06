<script>
	import { onMount } from "svelte";
	import { apiData, categories } from "./category.js";
	import { browser } from '$app/environment';
	
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
		})
    }

	async function AddCategory(event, element){
		// Check if the key pressed is Enter without 'event'
		const categoryName = element.value;

		const res = await fetch(`http://localhost:5000/Category?name=${categoryName}`, {
			method: 'PUT'
		})
		.then(response => {
			console.log(response.status);

			if (response.status === 200){
				GetCategories();
			}
			else{
				CreatePopupSomethingWentWrong(response.status);
			}
		})
		.catch(error => {
			console.log(error);
			return "something went wrong!";
		})
	}

	async function RemoveCategory(event, element){
		// Check if the key pressed is Enter without 'event'
		const categoryId = element.value;

		const res = await fetch(`http://localhost:5000/Category?id=${categoryId}`, {
			method: 'DELETE'
		})
		.then(response => {
			console.log(response.status);

			if (response.status === 200){
				GetCategories();
			}
			else{
				CreatePopupSomethingWentWrong(response.status);
			}
		})
		.catch(error => {
			console.log(error);
			return "something went wrong!";
		})
	}

	async function UpdateCategory(event, element){

		console.log('updateCategory was called');

		if (doc)
		{
			console.log('doc exists');
		}
		else
		{
			console.log('doc does not exist');
		}

		const categoryId = element.value;
		const categoryName = element.value;

		console.log(element.children);

		const res = await fetch(`http://localhost:5000/Category`, {
			method: 'POST',
			body: JSON.stringify({id: categoryId, name: categoryName}),
		})
		.then(response => {
			console.log(response.status);

			if (response.status === 200){
				GetCategories();
			}
			else{
				CreatePopupSomethingWentWrong(response.status);
			}
		})
		.catch(error => {
			console.log(error);
			return "something went wrong!";
		})
	}

	function CreatePopupSomethingWentWrong(statusCode){
		alert(`Your request returned ${statusCode}!`);
	}
</script>

<h1>Categories</h1>
<p>All categories: <button on:click={GetCategories}>Fetch</button></p>
<p><input type="text" placeholder="Category name" on:keyup|preventDefault={e => e.key === 'Enter' && AddCategory(e, e.target)}></p>
<p><input type="text" pattern="[0-9]" on:keyup|preventDefault={e => e.key === 'Enter' && RemoveCategory(e, e.target)}></p>

<form onsubmit="{UpdateCategory(this)}">
	<input id="category_id" type="text" placeholder="Category id">
	<input id="category_name" type="text" placeholder="Category name">
	<button type="submit">Update</button>
</form>

<table>
	<tr>
		<th>Id</th>
		<th>Name</th>
	</tr>
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
