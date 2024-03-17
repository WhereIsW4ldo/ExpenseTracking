<script>
	export let categories = {
		'1': 'Food',
		'2': 'House'
	};

	async function GetCategories() {
        fetch ("http://localhost:5298/CategoryController/")
        .then(response => response.json)
        .then(data => {
            console.log(data);
            categories = data;
        })
    }
</script>

<h1>Categories</h1>
<p>All categories: <button on:click={GetCategories}>Fetch</button></p>

<table>
	<th>
		<tc> Id </tc>
		<tc> Name </tc>
	</th>
	{#each Object.entries(categories) as [id, name]}
		<tr>
			<tc>
				{id}
			</tc>
			<tc>
				{name}
			</tc>
		</tr>
	{/each}
</table>

<style>
</style>
