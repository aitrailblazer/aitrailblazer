# Use Go 1.23 image for the build stage but with an older glibc
FROM golang:1.23-bullseye as builder

# Set the working directory inside the container
WORKDIR /app

# Copy Go module files and download dependencies
COPY go.mod go.sum ./
RUN go mod download

# Copy the source code and build the application for Linux AMD64, disabling CGO to avoid glibc issues
COPY . .
RUN CGO_ENABLED=0 GOARCH=amd64 GOOS=linux go build -o app

# Use a base image with wkhtmltopdf pre-installed that aligns with older glibc versions
FROM openlabs/docker-wkhtmltopdf-aas:latest

# Set the working directory
WORKDIR /app

# Copy the Go binary from the builder stage
COPY --from=builder /app/app .

# Ensure the binary is executable
RUN chmod +x app

# Expose the application port
EXPOSE 8001

# Run the Go application
ENTRYPOINT ["./app"]