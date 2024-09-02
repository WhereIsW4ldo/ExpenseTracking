import type { Category } from '$lib/Category/ViewModels/Category';

export async function getCategoryList(): Promise<Category[]> {
	return await fetch('http://localhost:5000/category')
		.then((response) => response.json())
		.then((data) => {
			console.log('Success:', data);
			return data as Category[];
		})
		.catch((error) => {
			console.error('Error:', error);
			return [];
		});
}

export async function updateCategory(id: number, name: string): Promise<boolean> {
	let response = await fetch(`http://localhost:5000/category?id=${id}&name=${name}`, { method: 'PUT' })
		.then(response => response.json())
		.then(data => console.log(data))
		.catch(error => {
		console.error('Error:', error);
	});

	return true;
}

export async function deleteCategory(category: Category): Promise<Category> {
	return await fetch(`http://localhost:5000/category/${category.id}`, { method: 'DELETE' })
		.then(response => response.json())
		.then(data => {
			console.log(data)
			return data as Category;
		})
		.catch(error => {
		console.error('Error:', error);
		return {
			id: 0,
			name: 'Error'
		};
	});
}

export async function addCategory(name: string): Promise<Category> {
	return await fetch(`http://localhost:5000/category/${name}`, { method: 'POST' })
		.then(response => response.json())
		.then(data => {
			console.log(data)
			return data as Category;
		})
		.catch((error) => {
			console.error('Error:', error);
			return {
				id: 0,
				name: 'Error'
			};
		});
}