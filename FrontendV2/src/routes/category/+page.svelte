<script lang="ts">
	import Header from '$lib/Header.svelte';
	import type { Category } from '$lib/Category/ViewModels/Category';
	import { addCategory, deleteCategory, getCategoryList, updateCategory } from '$lib/Category/CategoryService';
	import { onMount } from 'svelte';

	let categories: Category[] = [];
	let showUpdateDialog: boolean = false;
	let selectedCategory: Category;

	onMount(async () => {
		categories = await getCategoryList();
	});

	async function handleAddCategory(event: Event) {
		event.preventDefault();
		const form = event.target as HTMLFormElement;
		const formData = new FormData(form);
		const name = formData.get('name') as string;

		const category = await addCategory(name);
		categories = [...categories, category];
	}

	async function handleRemoveCategory(category: Category) {
		let removedCategory = await deleteCategory(category);
		categories = categories.filter((category) => category.id !== removedCategory.id);
	}

	async function handleUpdateCategory(event: Event) {
		event.preventDefault();
		const form = event.target as HTMLFormElement;
		const formData = new FormData(form);
		const name = formData.get('name') as string;
		const idString = formData.get('id') as string;
		const id = +idString;

		await updateCategory(id, name)
			.then(async () => {
				categories = await getCategoryList();
			});

		showUpdateDialog = false;
	}
</script>

<style>
	table {
		width: 100%;
	}

	td {
		border: 1px solid black;
			border-radius: 5px;
	}

	.header {
		background-color: #979494;
	}
</style>

<Header />

<h1>Category page</h1>

<form on:submit={handleAddCategory}>

	<label>Name
		<input name="name">
	</label>
	<button>
		Add
	</button>
</form>

{#if showUpdateDialog}
<dialog open>
	<p>Edit category</p>
	<form on:submit={handleUpdateCategory}>
		<label>ID
			<input name="id" readonly value="{selectedCategory.id}">
		</label>
		<label>Name
			<input name="name" value="{selectedCategory.name}">
		</label>
		<button>
			Update
		</button>
	</form>
</dialog>
{/if}

<table>
	<tr class="header">
		<td>
			ID
		</td>
		<td>
			Name
		</td>
		<td />
	</tr>
	{#each categories as category}
		<tr>
			<td>{category.id}</td>
			<td>{category.name}</td>
			<td>
				<button on:click={async () => { await handleRemoveCategory(category) }}> Remove </button>
				<button on:click={() => {
					showUpdateDialog = true;
					selectedCategory = category;
				}}> Update </button>
			</td>
		</tr>
	{/each}
</table>

