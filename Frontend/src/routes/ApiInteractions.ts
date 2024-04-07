import { writable, type Writable } from 'svelte/store';

export let categories: Writable<Category[]> = writable([]);

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
