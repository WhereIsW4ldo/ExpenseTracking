export class Category {
	public id: number;
	public name: string;

	constructor(id: number, name: string) {
		this.id = id;
		this.name = name;
	}
}

export async function GetCategories(): Promise<Category[]> {
	return await fetch('http://localhost:5000/Category')
		.then((response) => response.json())
		.then((cats: Category[]) => {
			console.log(cats);
			return cats.sort((a, b) => a.id - b.id);
		});
}

export async function AddCategory(categoryName: string) {
	if (categoryName === '' || categoryName == null) return;

	await fetch(`http://localhost:5000/Category?name=${categoryName}`, {
		method: 'PUT'
	}).then((response) => {
		console.log(response.status);
	});
}

export async function RemoveCategory(categoryId: number) {
	await fetch(`http://localhost:5000/Category?id=${categoryId}`, {
		method: 'DELETE'
	}).then((response) => {
		console.log(response.status);
	});
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
}
