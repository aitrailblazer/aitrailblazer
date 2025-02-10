# Use Go 1.23 image for building
FROM golang:1.23-bullseye AS builder

# Set the working directory
WORKDIR /app

# Copy Go module files first (leveraging caching)
COPY go.mod go.sum ./
RUN go mod download

# Copy the rest of the application
COPY . .

# Build the application (static binary)
RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o app

# Use a minimal base image for final deployment
FROM scratch

# Set the working directory
WORKDIR /app

# Copy CA certificates (needed for HTTPS requests)
COPY --from=builder /etc/ssl/certs/ca-certificates.crt /etc/ssl/certs/

# Copy the Go binary
COPY --from=builder /app/app .

# Copy the data directory (if needed)
COPY --from=builder /app/data ./data

COPY --from=builder /app/us-gaap-2024 ./us-gaap-2024

# Expose the application's port
EXPOSE 8001

# Run the Go application
ENTRYPOINT ["/app/app"]