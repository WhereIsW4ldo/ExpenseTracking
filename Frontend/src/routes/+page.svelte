<script lang="ts">
	import { onMount } from 'svelte';
	import { writable, type Writable } from 'svelte/store';

	export let categories: Writable<Category[]> = writable([]);
	export let transactions: Writable<Transaction[]> = writable([]);

	let value = '';
	let test = '';
	let submittedCategoryName: string = null;

	onMount(() => {
		GetCategories();
		GetTransactions();
	});

	$: AddCategory(submittedCategoryName);

	export class Category {
		public id: number;
		public name: string;

		constructor(id: number, name: string) {
			this.id = id;
			this.name = name;
		}
	}

	export async function GetCategories() {
		await fetch('http://localhost:5000/Category')
			.then((response) => response.json())
			.then((cats: Category[]) => {
				categories = writable(cats.sort((a, b) => a.id - b.id));
				console.log(cats);
			});
	}

	export async function AddCategory(categoryName: string) {
		if (categoryName === '' || categoryName == null) return;

		await fetch(`http://localhost:5000/Category?name=${categoryName}`, {
			method: 'PUT'
		}).then((response) => {
			console.log(response.status);
		});

		await GetCategories();
	}

	export async function RemoveCategory(categoryId: number) {
		await fetch(`http://localhost:5000/Category?id=${categoryId}`, {
			method: 'DELETE'
		}).then((response) => {
			console.log(response.status);
		});

		await GetCategories();
	}

	export async function EditCategory(categoryId: number, oldCategoryName: string) {
		let categoryName = prompt('Please enter the new category name: ', oldCategoryName);

		if (categoryName == null || categoryName == '') {
			console.log('user cancelled prompt!');
			return;
		}

		await fetch(`http://localhost:5000/Category?id=${categoryId}&name=${categoryName}`, {
			method: 'POST'
		}).then((response) => {
			console.log(response.status);
		});

		await GetCategories();
	}

	export class Transaction {
		public Id: number;
		public Amount: number;
		public Description: string;
		public Category: Category;
		public ExpenseDate: string;
		public CreationDate: string;

		constructor(id: number, amount: number, description: string, Category: Category, ExpenseDate: string, CreationDate: string) {
			this.Id = id;
			this.Amount = amount;
			this.Description = description;
			this.Category = Category;
			this.ExpenseDate = ExpenseDate;
			this.CreationDate = CreationDate;
		}
	}

	export async function GetTransactions() {
		await fetch('http://localhost:5000/Transaction')
			.then((response) => response.json())
			.then((trans: Transaction[]) => {
				console.log(trans);
				transactions = writable(trans.sort((a, b) => {
					let a_d = new Date(a);
					let b_d = new Date(b);
					return a_d.getTime() - b_d.getTime();
				}));
			});
	}

	export async function AddTransaction() {


		await fetch('http://localhost:5000/Transaction', {
			method: 'PUT'
		});
	}

	export async function ValidEnteredTransactionData(): boolean {
		return false;
	}

</script>

<div class="menuBar">
	<div class="menuItem">Overview</div>
	<div class="menuItem">Expenses</div>
	<div class="menuItem">Categories</div>
	<div class="menuIcon">
		<div class="bar"></div>
		<div class="bar"></div>
		<div class="bar"></div>
	</div>
</div>

<div class="main">
	<form on:submit|preventDefault={GetCategories}>
		<label
		>Get all categories:
			<button>Get</button>
		</label>
	</form>

	<h2>Categories</h2>

	<table>
		<thead>
		<tr>
			<th> Id</th>
			<th> Name</th>
		</tr>
		</thead>
		<tbody>
		{#each $categories as category}
			<tr>
				<td>
					{category.id}
				</td>
				<td>
					{category.name}
				</td>
				<td>
					<form
						on:submit|preventDefault={() => {
								RemoveCategory(category.id);
							}}
					>
						<button>Remove</button>
					</form>
					<form
						on:submit|preventDefault={() => {
								EditCategory(category.id, category.name);
							}}
					>
						<button>Edit</button>
					</form>
				</td>
			</tr>
		{/each}
		</tbody>
	</table>

	<form
		id="formAddCategory"
		on:submit|preventDefault={() => {
			submittedCategoryName = value;
			document.getElementById('addCategoryTextField').innerText = '';
		}}
	>
		<label>Add category: <input bind:value id="addCategoryTextField" /></label>
		<input form="formAddCategory" type="submit" value="Add" />
	</form>
</div>

<form id="transactionForm"
			on:submit|preventDefault={() => {
				if (ValidEnteredTransactionData()) {
					AddTransaction();
				}
			}}>
	<label>Amount: <input id="TransactionAmount" placeholder="amount" type="number"></label>
	<br />
	<label>Description: <textarea id="TransactionDescription" placeholder="description"></textarea></label>
	<br />
	<label>Category:
		<select id="TransactionCategory">
			{#each $categories as category}
				<option value="{category.id}">{category.name}</option>
			{/each}
		</select>
	</label>
	<br />
	<label>ExpenseDate: <input id="TransactionDate" type="date"></label>
	<br />
	<input type="submit" value="Add Transaction" />
</form>
<style>
    table,
    th,
    td {
        border: 1px solid black;
        border-radius: 20px;
        text-align: center;
    }

    th,
    td {
        padding: 10px;
    }

    th {
        background-color: rgba(219, 123, 11, 0.4);
    }

    tr {
        background-color: rgba(219, 123, 11, 0.4);
    }

    .menuBar {
        padding: 10px;
        padding-left: 50%;
        width: max-content;
        margin-bottom: 30px;
    }

    .menuIcon,
    .menuItem {
        float: inline-start;
        margin-right: 40px;
    }

    .main {
        background-color: rgba(255, 232, 163, 0.3);
        height: 600px;
        padding: 7%;
    }

    * {
        font-family: Verdana, Geneva, Tahoma, sans-serif;
    }

    h2 {
        color: rgb(219, 123, 11);
    }

    .bar {
        width: 20px;
        height: 2.5px;
        background-color: #333;
        margin: 3px 0;
        transition: 0.4s;
    }
</style>
