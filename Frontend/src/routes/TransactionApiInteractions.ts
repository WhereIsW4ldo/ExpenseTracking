import { Category } from './CategoryApiInteractions';

class Transaction {
	public id: number;
	public amount: number;
	public description: string;
	public category: Category;
	public expenseDate: string;
	public creationDate: string;

	constructor(
		id: number,
		amount: number,
		description: string,
		category: Category,
		expenseDate: string,
		creationDate: string
	) {
		this.id = id;
		this.amount = amount;
		this.description = description;
		this.category = category;
		this.expenseDate = expenseDate;
		this.creationDate = creationDate;
	}
}

export async function GetTransactions(): Promise<Transaction[]> {
	return await fetch('http://localhost:5000/Transaction')
		.then((response) => {
			return response.json();
		})
		.then((trans: Transaction[]) => {
			console.log(
				trans.sort((a, b) => {
					let a_d = new Date(a.toString());
					let b_d = new Date(b.toString());
					return a_d.getTime() - b_d.getTime();
				})
			);
			return trans.sort((a, b) => {
				let a_d = new Date(a.toString());
				let b_d = new Date(b.toString());
				return a_d.getTime() - b_d.getTime();
			});
		});
}

export async function AddTransaction(
	transactionInputAmount: number,
	transactionInputDescription: string,
	transactionInputCategory: number,
	transactionInputDate: string
) {
	var amount = encodeURIComponent(transactionInputAmount);
	var description = encodeURIComponent(transactionInputDescription);
	var category = encodeURIComponent(transactionInputCategory);
	var expenseDate = encodeURIComponent(transactionInputDate);

	var url = `http://localhost:5000/Transaction?amount=${amount}&description=${description}&categoryId=${category}&expenseDate=${expenseDate}`;

	await fetch(url, {
		method: 'PUT'
	}).then((response) => {
		console.log('added transaction: ', response.status);
	});
}
