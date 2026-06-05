#!/usr/bin/env bash
set -euo pipefail

# Render PlantUML diagram using Docker if available, otherwise Java+plantuml.jar (requires Java 11+ and Graphviz dot)
# Usage: ./scripts/render_plantuml.sh [path-to-puml]

ROOT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)
PUML=${1:-"$ROOT_DIR/diagrams/CineScopeClassDiagram.puml"}
OUTDIR="$ROOT_DIR/diagrams/output"
JAR="$ROOT_DIR/plantuml.jar"

mkdir -p "$OUTDIR"

if command -v docker >/dev/null 2>&1; then
  echo "Using Docker to render PlantUML..."
  docker run --rm -v "$ROOT_DIR":/workspace plantuml/plantuml -tpng "/workspace/${PUML#$ROOT_DIR/}" -o "/workspace/diagrams/output"
  echo "Rendered PNG(s) written to: $OUTDIR"
  exit 0
fi

if command -v java >/dev/null 2>&1; then
  echo "Java found. Checking version..."
  JAVAVER=$(java -XshowSettings:properties -version 2>&1 | awk -F '"' '/version/ {print $2}') || true
  MAJOR=$(echo "$JAVAVER" | awk -F. '{ if ($1 == "1") {print $2} else {print $1} }' | cut -d'-' -f1)
  if [ -z "$MAJOR" ] || [ "$MAJOR" -lt 11 ]; then
	echo "Java $JAVAVER detected. PlantUML requires Java 11+. Install Java 11+ or use Docker." >&2
	exit 2
  fi
  if [ -f "$JAR" ]; then
	if command -v dot >/dev/null 2>&1; then
	  echo "Running plantuml.jar to render..."
	  java -jar "$JAR" -tpng -o "$OUTDIR" "$PUML"
	  echo "Rendered PNG(s) written to: $OUTDIR"
	  exit 0
	else
	  echo "Graphviz 'dot' not found on PATH. Install graphviz or use Docker." >&2
	  exit 3
	fi
  else
	echo "plantuml.jar not found at $JAR. Place plantuml.jar in project root or use Docker." >&2
	exit 4
  fi
fi

echo "Neither Docker nor Java available. Install Docker, or Java 11+ and Graphviz." >&2
exit 5
