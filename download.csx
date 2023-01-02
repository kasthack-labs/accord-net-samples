var dir = @"sample-images";
var client = new HttpClient(new SocketsHttpHandler(){
	Proxy = new WebProxy("socks5://127.0.0.1", 9150),
});

foreach(var url in File.ReadLines(Path.Combine(dir, "images.txt")))
{
	var filename = Path.Combine(dir, url.Split("/").Last());
	if (File.Exists(filename))
		continue;
	url.Dump();
	try
	{
	await File.WriteAllBytesAsync(
		filename,
		await client.GetByteArrayAsync(url)
	);
	}
	catch{}
}
