<script lang="ts">
	import type { Writable } from 'svelte/store';
	import Modal from './Modal.svelte';
	import type { Category } from './CategoryApiInteractions';
	import { AddTransaction } from './TransactionApiInteractions';

	export let showModal: boolean = false;
	export let categories: Writable<Category[]>;

	export let transactionInputAmount = '';
	export let transactionInputDescription = '';
	export let transactionInputCategory = '';
	export let transactionInputDate = '';

	export function ValidEnteredTransactionData(): boolean {
		let amount = (document.getElementById('TransactionAmount') as HTMLInputElement)?.value;
		let category = (<HTMLSelectElement>document.getElementById('TransactionCategory'))?.value;
		let dateValue = (document.getElementById('TransactionDate') as HTMLInputElement)?.value;
		let description = (document.getElementById('TransactionDescription') as HTMLTextAreaElement)
			?.value;

		console.log('Entered Amount: ', amount);
		console.log('Category: ', category);
		console.log('Date: ', dateValue);

		let valid = true;

		if (amount == null || amount === '') {
			document.getElementById('TransactionAmountWarning')?.removeAttribute('hidden');
			valid = false;
		} else {
			document.getElementById('TransactionAmountWarning')?.setAttribute('hidden', 'true');
		}

		if (category == null || category === '') {
			document.getElementById('TransactionCategoryWarning')?.removeAttribute('hidden');
			valid = false;
		} else {
			document.getElementById('TransactionCategoryWarning')?.setAttribute('hidden', 'true');
		}

		if (dateValue == null || dateValue === '') {
			document.getElementById('TransactionDateWarning')?.removeAttribute('hidden');
			valid = false;
		} else {
			document.getElementById('TransactionDateWarning')?.setAttribute('hidden', 'true');
		}

		if (valid) {
			transactionInputAmount = amount;
			transactionInputDescription = description;
			transactionInputCategory = category;
			transactionInputDate = dateValue;
		} else {
			transactionInputAmount = '';
			transactionInputDescription = '';
			transactionInputCategory = '';
			transactionInputDate = '';
		}

		return valid;
	}
</script>

<Modal bind:showModal>
	<form
		id="transactionForm"
		on:submit|preventDefault={() => {
			if (ValidEnteredTransactionData()) {
				console.log('Valid transaction data!');
				AddTransaction(
					Number.parseFloat(transactionInputAmount),
					transactionInputDescription,
					Number.parseInt(transactionInputCategory),
					transactionInputDate
				);
			}
		}}
	>
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
</Modal>
