<script lang="ts">
	import { onMount } from 'svelte';
	import { writable, type Writable } from 'svelte/store';
	import type { Category } from './CategoryApiInteractions';
	import type { Transaction } from './TransactionApiInteractions';

	import TransactionPopup from './TransactionPopup.svelte';
	import Modal from './Modal.svelte';

	import {
		GetCategories,
		AddCategory,
		EditCategory,
		RemoveCategory
	} from './CategoryApiInteractions';
	import { GetTransactions, AddTransaction } from './TransactionApiInteractions';

	export let categories: Writable<Category[]> = writable([]);
	export let transactions: Writable<Transaction[]> = writable([]);

	let showModal: boolean = false;
	let showModal2: boolean = false;

	let value = '';
	let submittedCategoryName: string = '';

	onMount(async () => {
		categories = writable(await GetCategories());
		transactions = writable(await GetTransactions());
	});

	let transactionInputAmount = 10;
	let transactionInputDescription = '';
	let transactionInputCategory = 92;
	let transactionInputDate = '';

	$: AddCategory(submittedCategoryName);
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
	<form
		on:submit|preventDefault={async () => {
			categories = writable(await GetCategories());
		}}
	>
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
							on:submit|preventDefault={async () => {
								await RemoveCategory(category.id);
								categories = writable(await GetCategories());
							}}
						>
							<button>Remove</button>
						</form>
						<form
							on:submit|preventDefault={async () => {
								await EditCategory(category.id, category.name);
								categories = writable(await GetCategories());
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
		on:submit|preventDefault={async () => {
			let addCategoryField = document.getElementById('addCategoryTextField');
			if (addCategoryField === null) return;
			const addCategoryTextField = addCategoryField as HTMLInputElement;
			await AddCategory(addCategoryTextField.value);
			categories = writable(await GetCategories());
			addCategoryField.innerText = '';
		}}
	>
		<label>Add category: <input bind:value id="addCategoryTextField" /></label>
		<input form="formAddCategory" type="submit" value="Add" />
	</form>
</div>

<form>
	<label
		>Get all transactions:
		<button
			on:click|preventDefault={async () => {
				transactions = writable(await GetTransactions());
			}}>Get</button
		>
	</label>
</form>

<form>
	<button
		on:click|preventDefault={() => {
			showModal = true;
		}}>Show modal</button
	>
</form>

{#if showModal}
	<TransactionPopup
		bind:showModal
		bind:categories
		bind:initialAmount={transactionInputAmount}
		bind:initialDescription={transactionInputDescription}
		bind:initialCategory={transactionInputCategory}
		bind:initialDate={transactionInputDate}
	></TransactionPopup>
{/if}

<!-- <Modal bind:showModal>
	<form id="transactionForm" on:submit|preventDefault={() => {}}>
		<label
			>Amount: <input id="TransactionAmount" placeholder="amount" type="number" />
			<div id="TransactionAmountWarning" class="warning_text" hidden>
				Please fix your money amount!
			</div></label
		>
		<br />
		<label
			>Description: <textarea id="TransactionDescription" placeholder="description"
			></textarea></label
		>
		<br />
		<label
			>Category:
			<select id="TransactionCategory">
				{#each $categories as category}
					<option value={category.id}>{category.name}</option>
				{/each}
			</select>
			<div id="TransactionCategoryWarning" class="warning_text" hidden>
				Please fix your money category!
			</div>
		</label>
		<br />
		<label
			>ExpenseDate: <input id="TransactionDate" type="date" />
			<div id="TransactionDateWarning" class="warning_text" hidden>
				Please fix your money date!
			</div></label
		>
		<br />
		<input type="submit" value="Add Transaction" />
	</form>
</Modal> -->

<h2>Transactions</h2>
<table>
	<thead>
		<tr>
			<th>Id</th>
			<th>Amount</th>
			<th>Description</th>
			<th>Category</th>
			<th>Expense Date</th>
			<th>Creation Date</th>
		</tr></thead
	>
	<tbody>
		{#each $transactions as transaction}
			<tr>
				<td>{transaction.id}</td>
				<td>{transaction.amount}</td>
				<td>{transaction.description}</td>
				<td>{transaction.category.name}</td>
				<td>{transaction.expenseDate}</td>
				<td>{transaction.creationDate}</td>
			</tr>
		{/each}
	</tbody>
</table>

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
		/*noinspection CssInvalidPropertyValue*/
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

	.warning_text {
		color: red;
	}
</style>
