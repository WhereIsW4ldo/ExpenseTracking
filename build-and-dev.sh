echo "building API now"

# Build the API
docker build -t api -f Dockerfile.api .

# Run docker compose
docker compose up -d